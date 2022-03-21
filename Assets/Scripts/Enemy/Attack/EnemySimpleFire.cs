using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleFire : MonoBehaviour
{

    private AudioSource AS;

    [SerializeField] private AudioClip sound;

    [SerializeField] private AudioClip block;


    // Start is called before the first frame update
    void Start()
    {

        AS = GetComponent<AudioSource>();

        AS.PlayOneShot(sound);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Shields"))
        {
            AS.PlayOneShot(block);
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
