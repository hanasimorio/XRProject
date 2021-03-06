using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;

    [SerializeField]
    private GameObject button;

    private GameObject Presser;

    private bool IsPressed;

    private GameObject basebutton;

    AudioSource AS;

    [SerializeField] private AudioClip Push;

    // Start is called before the first frame update
    void Start()
    {
        IsPressed = false;
        var p = transform.parent.gameObject;
        basebutton = p.transform.parent.gameObject;

        AS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsPressed)
        {
            button.transform.localPosition = new Vector3(0f, 0.003f, 0f);
            Presser = other.gameObject;
            onPressed.Invoke();
            AS.PlayOneShot(Push);
            IsPressed = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {


        button.transform.localPosition = new Vector3(0f, 0.03f, 0f);
        onReleased.Invoke();
        IsPressed = false;

    }


    public void Destroy()
    {
        StartCoroutine(D());
    }


    IEnumerator D()
    {
        yield return new WaitForSeconds(1f);
        Destroy(basebutton);
    }
}
