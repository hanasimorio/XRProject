using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public InputActionReference Grip = null;
    
    private bool FirstGripOn = false;

    [SerializeField, Tooltip("�e�X�gEnemy")]
    private GameObject TestEnemy;

    [SerializeField, Tooltip("���@�̐�����\������Image")]
    private GameObject TutoImage;

    [SerializeField, Tooltip("�����@Sprite")]
    private Sprite Flamesprite;

    [SerializeField, Tooltip("�����@Sprite")]
    private Sprite Thundersprite;

    [SerializeField, Tooltip("�����@Sprite")]
    private Sprite Starsprite;

    [SerializeField, Tooltip("�����@Sprite")]
    private Sprite Watersprite;

    [SerializeField, Tooltip("�y���@Sprite")]
    private Sprite Claysprite;

    [SerializeField] private GameObject SpawnPoint;

    Image image;

    private bool TutorialOn = false;

    private GameObject Test;

    private int ClayCount = 0;

    public UnityEvent TutorialClear;

    private bool IsThunder = false;

    void Start()
    {
        image = TutoImage.GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float value = Grip.action.ReadValue<float>();
        if (TutorialOn)
        {
            if (value >= 1)
            {
                if (!FirstGripOn)
                {
                    FirstGripOn = true;
                    StartCoroutine(SpawnTestBody());
                }
            }
        }
        
        if(ClayCount >= 2)
        {
            TutorialClear.Invoke();
            Destroy(Test);
            Destroy(image);
            Debug.Log("TutorialClear!!");
        }

    }

    public void StartTutorial()
    {
        TutorialOn = true;
    }
    IEnumerator SpawnTestBody()
    {
        yield return new WaitForSeconds(3f);
        Test = Instantiate(TestEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        StartCoroutine(Flame());
    }


    IEnumerator Flame()
    {
        yield return new WaitForSeconds(3f);
        image.sprite = Flamesprite;
        image.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator Thunder()
    {
        yield return new WaitForSeconds(3f);
        Destroy(Test);
        Test = Instantiate(TestEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        image.sprite = Thundersprite;
    }

    public IEnumerator Star()
    {
        if (!IsThunder)
        {
            IsThunder = true;
            yield return new WaitForSeconds(3f);
            Destroy(Test);
            Test = Instantiate(TestEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            image.sprite = Starsprite;
        }
    }

    public IEnumerator Water()
    {
        yield return new WaitForSeconds(3f);
        Destroy(Test);
        Test = Instantiate(TestEnemy, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        image.sprite = Watersprite;
    }
    public IEnumerator Clay()
    {
        yield return new WaitForSeconds(3f);
        image.sprite = Claysprite;
    }

    public void HitClay(int count)
    {
        ClayCount += count;
    }


}
