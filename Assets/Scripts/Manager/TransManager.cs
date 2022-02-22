using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransManager : MonoBehaviour
{

    private GameObject root;


    [SerializeField,Tooltip("ƒ‰ƒCƒg")] private GameObject DL;

    [SerializeField] private GameObject Tutorial;

    private TutorialManager tm;

    private GameObject[] obj = new GameObject[10] ;
    void Start()
    {

        root = GameObject.FindGameObjectWithTag("Trans");
        if (root != null)
        {
            for (int i = 0; i < root.transform.childCount; i++)
            {
                
                obj[i] = root.transform.GetChild(i).gameObject;
               
            }

            tm = Tutorial.GetComponent<TutorialManager>();

        }
        else Destroy(this.gameObject);

        StartMove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMove()
    {
        StartCoroutine(StartTrans());
    }



    private IEnumerator StartTrans()
    {
        yield return new WaitForSeconds(2f);

        var p = obj[0].GetComponent<TransObject>();//1
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(3);

        p = obj[1].GetComponent<TransObject>();//2
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(3);

        p = obj[2].GetComponent<TransObject>();//3
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(2);

        p = obj[3].GetComponent<TransObject>();//4
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(2);

        p = obj[4].GetComponent<TransObject>();//5
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(3);

        p = obj[5].GetComponent<TransObject>();//6
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(3);

        p = obj[6].GetComponent<TransObject>();//7
        StartCoroutine(p.Move(18));
        yield return new WaitForSeconds(6);

        p = obj[7].GetComponent<TransObject>();//8
        StartCoroutine(p.Move(22));
        yield return new WaitForSeconds(10);

        p = obj[8].GetComponent<TransObject>();//9
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(20f);

        DL.SetActive(true);
        p = obj[9].GetComponent<TransObject>();//10
        StartCoroutine(p.Move(100));
        tm.ShowFVoice();
        yield return new WaitForSeconds(20f);

        yield return new WaitForSeconds(10f);

        Destroy(this.gameObject);
        

    }


}
