using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour,IDamage
{
    private GameObject Tutorial;
    private TutorialManager tuto;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private Transform ShotPos;

    private float dame = 0;

    // Start is called before the first frame update
    void Start()
    {
        Tutorial = GameObject.Find("TutorialManager");
        if(Tutorial != null)
        {
            tuto = Tutorial.GetComponent<TutorialManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void ApplyDamage(float damage, List<Type> types)
    {
        dame = damage;

        for (int i = 0; i < types.Count; i++)
        {
            switch (types[i])
            {
                case Type.Flame:
                    StartCoroutine(tuto.Thunder());
                    break;
                case Type.Thunder:
                    StartCoroutine(tuto.Star());
                    break;
                case Type.Explosion:
                    StartCoroutine(tuto.Water());
                    break;
                case Type.Water:
                    StartCoroutine(Shot());
                    StartCoroutine(tuto.Clay());
                    break;
            }
        }
    }

   IEnumerator Shot()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(Bullet, ShotPos.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(ShotPos.forward * 500);
        StartCoroutine(Shot());
    }


}
