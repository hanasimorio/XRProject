using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrayController : MonoBehaviour
{

    private Rigidbody rb;
    //[SerializeField] private GameObject Parent;
    private bool col = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SetParent(GameObject hand)
    {
        var emptyObject = new GameObject();
        emptyObject.transform.parent = hand.gameObject.transform;
        transform.parent = emptyObject.transform;
        col = true;
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (col == false)
            StartCoroutine(SetParent(other.gameObject));
    }

}
