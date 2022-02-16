using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoLine : MonoBehaviour
{

    private bool CanLine = false;

    private Animator ani;

    private LineRenderer line;

    private int positionCount;

    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        line = gameObject.GetComponent<LineRenderer>();
        StartCoroutine(Water());
    }

    // Update is called once per frame
    void Update()
    {
        if(CanLine)
        {
            positionCount++;
            line.positionCount = positionCount;
            line.SetPosition(positionCount - 1, gameObject.transform.position);
        }


    }

    void LineOn()
    {
        CanLine = true;
    }

    void End()
    {
        positionCount = 0;
        Debug.Log("end");
        CanLine = false;
    }

    IEnumerator Flame()
    {
        yield return new WaitForSeconds(8);
        ani.SetTrigger("Flame");
        StartCoroutine(Thunder());
    }

    IEnumerator Thunder()
    {
        yield return new WaitForSeconds(8);
        ani.SetTrigger("Thunder");
        StartCoroutine(Star());

    }

    IEnumerator Star()
    {
        yield return new WaitForSeconds(8);
        ani.SetTrigger("Star");
        StartCoroutine(Clay());
    }

    IEnumerator Clay()
    {
        yield return new WaitForSeconds(8);
        ani.SetTrigger("Clay");
        StartCoroutine(Water());
    }

 

    IEnumerator Water()
    {
        yield return new WaitForSeconds(8);
        ani.SetTrigger("Water");
        StartCoroutine(Flame());
    }


}
