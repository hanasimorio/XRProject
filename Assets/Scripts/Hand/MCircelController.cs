using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCircelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�E��ɓ���������
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RightHand" || other.gameObject.tag == "LeftHand")
        {
            Destroy(gameObject);
        }
    }



}
