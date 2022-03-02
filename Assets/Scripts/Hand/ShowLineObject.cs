using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLineObject : MonoBehaviour
{

    [SerializeField, Tooltip("LineObject")]
    private GameObject LinePos;


    // Start is called before the first frame update
    void Start()
    {
        LinePos.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Online()
    {
        LinePos.SetActive(true);
        Debug.Log("ON");
    }

}
