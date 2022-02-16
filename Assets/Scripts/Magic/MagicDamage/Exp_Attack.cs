using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var Enemy = other.gameObject.GetComponent<IDamage>();
        if(Enemy != null)
        {
            List<Type> types = new List<Type> { Type.Explosion };
            Enemy.ApplyDamage(100f, types);
            Destroy(this.gameObject);
        }
    }

}
