using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleFire : MonoBehaviour
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
        if(other.gameObject.CompareTag("Shields"))
        {
            Destroy(this.gameObject);
        }
        else
        {
            var Player = other.gameObject.GetComponent<IPlayerDamage>();
            if (Player != null)
            {
                List<Attacks> attacks = new List<Attacks> { Attacks.Flame };
                Player.ApplyDamage(20f, attacks);
                Destroy(this.gameObject);
            }
        }
    }

}
