using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoEnemyBullet : MonoBehaviour
{
    private GameObject Tutorial;
    private TutorialManager tuto;

    // Start is called before the first frame update
    void Start()
    {
        Tutorial = GameObject.Find("TutorialManager");
        if (Tutorial != null)
        {
            tuto = Tutorial.GetComponent<TutorialManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shields"))
        {
            tuto.HitClay(1);
            Destroy(gameObject);
        }
        else
        {
            var Player = other.gameObject.GetComponent<IPlayerDamage>();
            if (Player != null)
            {
                List<Attacks> attacks = new List<Attacks> { Attacks.Flame };
                Player.ApplyDamage(0f, attacks);
                Destroy(this.gameObject);
            }
        }
    }


}
