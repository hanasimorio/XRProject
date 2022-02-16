using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFeedback : MonoBehaviour
{

    public  bool keyhit = false;
    public  bool KeyAgain = false;

    private float originalYposition;


    // Start is called before the first frame update
    void Start()
    {
        originalYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyhit)
        {
            KeyAgain = false;
            keyhit = false;
            transform.position += new Vector3(0f, -0.03f, 0f);

        }
        if(transform.position.y < originalYposition)
        {
            transform.position += new Vector3(0f, 0.005f, 0f);
        }
        else
        {
            KeyAgain = true;
        }

    }
}
