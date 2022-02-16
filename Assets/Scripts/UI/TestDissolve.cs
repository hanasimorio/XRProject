using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDissolve : MonoBehaviour
{

    private Material mate;

    // Start is called before the first frame update
    void Start()
    {
        mate = GetComponent<MeshRenderer>().material;
        mate.SetFloat("_Threshold", 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
