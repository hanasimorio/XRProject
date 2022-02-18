using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObject : MonoBehaviour
{

    Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator Move(float delay)
    {
        ani.SetTrigger("Move");
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }



}
