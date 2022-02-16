using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("��")]
    private GameObject Sky;

    [SerializeField, Tooltip("�n��")]
    private GameObject Terrain;

    [SerializeField, Tooltip("����")]
    private GameObject Props;

    [SerializeField, Tooltip("��")]
    private GameObject Floor;



    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowStage(float wait)
    {
        StartCoroutine(WaitShow(wait));
    }

    IEnumerator WaitShow(float WaitSeconds)
    {
        yield return new WaitForSeconds(WaitSeconds);
        Destroy(Floor);
        Sky.SetActive(true);
        Terrain.SetActive(true);
        Props.SetActive(true);
    }

    

}
