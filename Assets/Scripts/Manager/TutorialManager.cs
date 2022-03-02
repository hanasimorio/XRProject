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

    [SerializeField, Tooltip("テストEnemy")]
    private GameObject TestEnemy;

    [SerializeField, Tooltip("魔法の説明を表示するImage")]
    private GameObject TutoImage;

    

    [SerializeField, Tooltip("炎魔法Sprite")]
    private Sprite Flamesprite;

    [SerializeField, Tooltip("雷魔法Sprite")]
    private Sprite Thundersprite;

    [SerializeField, Tooltip("星魔法Sprite")]
    private Sprite Starsprite;

    [SerializeField, Tooltip("水魔法Sprite")]
    private Sprite Watersprite;

    [SerializeField, Tooltip("土魔法Sprite")]
    private Sprite Claysprite;

    [SerializeField] private GameObject SpawnPoint;

    Image image;

    private bool TutorialOn = false;

    private GameObject Test;

    private int ClayCount = 0;

    public UnityEvent TutorialClear;

    private bool IsThunder = false;

    /*private AudioSource AS;

    [SerializeField, Tooltip("自己紹介")] private AudioClip Sound1;

    [SerializeField, Tooltip("チュートリアルを受けるかどうか")] private AudioClip Sound2;

    [SerializeField, Tooltip("線を書かせる")] private AudioClip Sound3;

    [SerializeField, Tooltip("火の魔法を書かせる")] private AudioClip Sound4;

    [SerializeField, Tooltip("火の魔法に触れさせる")] private AudioClip Sound5;

    [SerializeField, Tooltip("トリガーをひかせる")] private AudioClip Sound6;

    [SerializeField, Tooltip("火魔法の説明")] private AudioClip Sound7;

    [SerializeField, Tooltip("雷魔法を書かせる")] private AudioClip Sound8;

    [SerializeField, Tooltip("雷魔法の説明")] private AudioClip Sound9;

    [SerializeField, Tooltip("星魔法をかかせる")] private AudioClip Sound10;

    [SerializeField, Tooltip("星魔法の説明")] private AudioClip Sound11;

    [SerializeField, Tooltip("水魔法を書かせる")] private AudioClip Sound12;

    [SerializeField, Tooltip("水魔法の説明")] private AudioClip Sound13;

    [SerializeField, Tooltip("土魔法を書かせる")] private AudioClip Sound14;

    [SerializeField, Tooltip("盾魔法の説明")] private AudioClip Sound15;

    [SerializeField, Tooltip("チュートリアル終了")] private AudioClip Sound16;

    [SerializeField, Tooltip("移動開始の合図")] private AudioClip Sound17;*/

    void Start()
    {
        image = TutoImage.GetComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0f);

        //AS = GetComponent<AudioSource>();
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
        //AS.PlayOneShot(Sound3);
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

    public void ShowFVoice()
    {
        StartCoroutine(FirstVoice());
    }

    IEnumerator FirstVoice()
    {
        //AS.PlayOneShot(Sound1);
        yield return new WaitForSeconds(3f);
        //AS.PlayOneShot(Sound2);
    }

}
