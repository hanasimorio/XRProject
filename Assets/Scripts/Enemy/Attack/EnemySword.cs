using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
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

        var Player = other.gameObject.GetComponent<IPlayerDamage>();
        if (Player != null)
        {
            List<Attacks> attacks = new List<Attacks> { Attacks.Slash };
            Player.ApplyDamage(30f, attacks);
        }
    }
}

