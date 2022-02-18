using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransManager : MonoBehaviour
{

    private GameObject root;


    [SerializeField,Tooltip("ƒ‰ƒCƒg")] private GameObject DL;

    private GameObject[] obj = new GameObject[9] ;
    void Start()
    {

        root = GameObject.FindGameObjectWithTag("Trans");
        if (root != null)
        {
            for (int i = 0; i < root.transform.childCount; i++)
            {
                
                obj[i] = root.transform.GetChild(i).gameObject;
               
            }
        }
        else Destroy(this.gameObject);


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
        yield return new WaitForSeconds(4);

        p = obj[1].GetComponent<TransObject>();//2
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(4);

        p = obj[2].GetComponent<TransObject>();//3
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(3);

        p = obj[3].GetComponent<TransObject>();//4
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(4);

        p = obj[4].GetComponent<TransObject>();//5
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(5);

        p = obj[5].GetComponent<TransObject>();//6
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(5);

        p = obj[6].GetComponent<TransObject>();//7
        StartCoroutine(p.Move(18));
        yield return new WaitForSeconds(10);

        p = obj[7].GetComponent<TransObject>();//8
        StartCoroutine(p.Move(35));
        yield return new WaitForSeconds(20);

        p = obj[8].GetComponent<TransObject>();//9
        StartCoroutine(p.Move(5));
        yield return new WaitForSeconds(20);

        DL.SetActive(true);

        yield return new WaitForSeconds(10f);

        Destroy(this.gameObject);
        

    }


}
