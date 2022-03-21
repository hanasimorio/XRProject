using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewSceneManager : MonoBehaviour
{

    [SerializeField] private Image im;

    // Start is called before the first frame update
    void Start()
    {
        im.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadTitle()
    {
        StartCoroutine(title());

    }

    public void LoadMain()
    {
        StartCoroutine(main());

    }


    IEnumerator title()
    {
        for (float i = 0f; i > 1.0f; i += 0.02f)
        {
            im.color = new Color(0, 0, 0, i);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Titlecene");
    }


    IEnumerator main()
    {
        for (float i = 0f; i > 1.0f; i += 0.02f)
        {
            im.color = new Color(0, 0, 0, i);
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainScene");
    }


}
