using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransManager : MonoBehaviour
{
    private GameObject[] obj;
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            obj = GameObject.FindGameObjectsWithTag("Trans");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
