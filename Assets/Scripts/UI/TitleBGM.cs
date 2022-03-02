using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{

    AudioSource AS;


    // Start is called before the first frame update
    void Start()
    {
        AS.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBGM()
    {
        AS.Play();
        for (float i = 0; AS.volume < 0.5f; i += 0.1f)
        {
            AS.volume = i;
        }
    }

}
