using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFeedback : MonoBehaviour
{

    public  bool keyhit = false;
    public  bool KeyAgain = false;

    private float originalYposition;

    AudioSource AS;

    [SerializeField] private AudioClip Click;


    // Start is called before the first frame update
    void Start()
    {
        originalYposition = transform.position.y;
        var parent = gameObject.transform.root.gameObject;
        AS = parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyhit)
        {
            KeyAgain = false;
            keyhit = false;
            transform.position += new Vector3(0f, -0.03f, 0f);
            AS.PlayOneShot(Click);
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
