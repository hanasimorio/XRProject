using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("ãÛ")]
    private GameObject Sky;

    [SerializeField, Tooltip("ínñ ")]
    private GameObject Terrain;

    [SerializeField, Tooltip("åöï®")]
    private GameObject Props;

    [SerializeField, Tooltip("è∞")]
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
