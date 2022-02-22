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
        for (float i = 0.2f; i > 1.0f; i += 0.2f)
        {
            im.color = new Color(0, 0, 0, i);
        }

        SceneManager.LoadScene("Titlecene");

    }

    public void LoadMain()
    {
        for (float i = 0.2f; i > 1.0f; i += 0.2f)
        {
            im.color = new Color(0, 0, 0, i);
        }

        SceneManager.LoadScene("MainScene");

    }



}
