using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IDamage
{

    [SerializeField] private float HP = 100;

    [Tooltip("PlayerObject")]
    [SerializeField] private GameObject Player;

    [Tooltip("攻撃距離")]
    [SerializeField] private float Distance = 3;

    [SerializeField, Tooltip("DeadParticle")]
    private ParticleSystem particle;

    private Vector3 PlayerPosition;

    [SerializeField,Tooltip("スピード")]
    private float speed; //水魔法によって変化する

    private float step;

    private Animator ani;

    private bool AT = true;

    private Material material;

    private bool die = false;

    private Rigidbody rb;

    private float Threshold = 0;

    public GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();
        ani.SetBool("Walk", true);
        PlayerPosition = Player.transform.position;

        material = child.GetComponent<SkinnedMeshRenderer>().material;//Dissolveするためmaterial情報を取得

        rb = gameObject.GetComponent<Rigidbody>();

        rb.isKinematic = false;

        //GameManager.instance.SpawnCountUp();
        //StartCoroutine(TestSpawner());
          }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0&& !die)
        {
            StartCoroutine(Dead());
            //GameManager.instance.EnemyDead();
        }

        float distance = Vector3.Distance(Player.transform.position, this.gameObject.transform.position);
        if(distance > Distance)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition, step);
            //rb.isKinematic = false;
        }
        else if(AT)
        {
            speed = 0;
            rb.isKinematic = true;
            ani.SetBool("Walk", false);
            StartCoroutine(Attack());
        }
        Vector3 vector3 = Player.transform.position - this.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation = quaternion;


    }

    public void ApplyDamage(float damage,List<Type> types)
    {
        HP -= damage;
        ani.SetTrigger("Damage");

        for(int i = 0; i < types.Count; i++)
        {
            switch(types[i])
            {
                case Type.Flame:
                    StartCoroutine(FlameDamage(10));
                    break;
                case Type.Thunder:
                    //雷攻撃の効果処理
                    break;
                case Type.Water:
                    StartCoroutine(Waterbaff());
                    break;
            }
        }
    }

    IEnumerator Attack()
    {
        AT = false;
        ani.SetTrigger("Attack");
        yield return new WaitForSeconds(5f);
        AT = true;
    }
    IEnumerator FlameDamage(float Fdamage)//火魔法を食らった
    {
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
    }

    IEnumerator Waterbaff()//水魔法の範囲内に入った
    {
        speed = 0.01f;
        yield return new WaitForSeconds(5f);
        speed = 2f;
    }

    IEnumerator Dead()//HPが０死んだ
    {
        ani.SetBool("Die", true);
        die = true;
        speed = 0f;
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(2f);
        while(Threshold < 1.1)
        {
            yield return new WaitForSeconds(0.1f);
            Threshold += Time.fixedDeltaTime;
            Debug.Log(Threshold);
            material.SetFloat("_Threshold", Threshold);
        }
        Destroy(gameObject);
    }

    IEnumerator TestSpawner()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

 

}
