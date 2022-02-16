using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    [SerializeField] private GameObject WaterMagic;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            Instantiate(WaterMagic, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    

}
