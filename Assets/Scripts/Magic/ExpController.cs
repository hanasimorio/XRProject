using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    
    Rigidbody rb;
    [SerializeField] private GameObject P_Explosion;
    [SerializeField] float m_force = 20;
    [SerializeField] float m_radius = 5;
    [SerializeField] float m_upwards = 0;
    private Vector3 m_position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Exp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //五秒後に落下させるコルーチン
    IEnumerator Exp()
    {
        yield return new WaitForSeconds(5);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
    }


    //地面に当たったら爆発
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor")
        {
            Instantiate(P_Explosion, gameObject.transform.position, Quaternion.identity);
            Explosion();
            Destroy(gameObject);
        }
    }

    public void Explosion()
    {
        m_position = gameObject.transform.position;

        // 範囲内のRigidbodyにAddExplosionForce
        Collider[] hitColliders = Physics.OverlapSphere(m_position, m_radius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(m_force, m_position, m_radius, m_upwards, ForceMode.Impulse);
            }
        }
    }



}
