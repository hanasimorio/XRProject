using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimController : MonoBehaviour
{
    private Animator ani;

    public InputActionReference GripOn = null;



    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = GripOn.action.ReadValue<float>();

        if(value >= 1)
        {
            ani.SetBool("Grip", true);
        }
        else
        {
            ani.SetBool("Grip", false);
        }
    }
}
