using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObject : MonoBehaviour
{

    Animator ani;

    AudioSource AS;

    [SerializeField,Tooltip("‚µ‚ã‚Á")] private AudioClip sound1;
    [SerializeField, Tooltip("ƒoƒ^ƒb")] private AudioClip sound2;


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
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

    public void DontDestroyMove()
    {
        ani.SetTrigger("Move");
    }

    private void shotSound1()
    {
        AS.PlayOneShot(sound1);
    }

    private void shotsound2()
    {
        AS.PlayOneShot(sound2);
    }

}
