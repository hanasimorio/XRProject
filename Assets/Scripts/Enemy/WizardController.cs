using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour,IDamage
{
    [SerializeField] private float HP = 100;

    [Tooltip("PlayerObject")]
    private GameObject Player;

    [SerializeField, Tooltip("死んだときのパーティクル")]
    private ParticleSystem particle;

    private Animator ani;

    private bool AT = true;

    private bool die = false;

    private AudioSource AS;

    [SerializeField] private AudioClip sound;

    /// <summary>
    /// 射出するオブジェクト
    /// </summary>
    [SerializeField, Tooltip("射出するオブジェクトをここに割り当てる")]
    private GameObject ThrowingObject;

    /// <summary>
    /// 標的のオブジェクト
    /// </summary>
    [SerializeField, Tooltip("標的のオブジェクトをここに割り当てる")]
    private GameObject TargetObject;

    /// <summary>
    /// 射出角度
    /// </summary>
    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    /// <summary>
    /// 射出ポイント
    /// </summary>
    [SerializeField, Tooltip("射出するポジション")]
    private GameObject ShotPosition;

    private Material material;

    private float Threshold = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        ani = gameObject.GetComponent<Animator>();

        GameObject child = transform.GetChild(1).gameObject;

        material = child.GetComponent<SkinnedMeshRenderer>().material;

        AS =  GetComponent<AudioSource>();

        //GameManager.instance.SpawnCountUp();
        //StartCoroutine(TestSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && !die)
        {
            StartCoroutine(Dead());
            //GameManager.instance.EnemyDead();
        }
        else if(AT)
        {
            StartCoroutine(Attack());
        }

        Vector3 vector3 = Player.transform.position - this.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        transform.rotation = quaternion;


    }

    public void ApplyDamage(float damage, List<Type> types)
    {
        HP -= damage;
        ani.SetTrigger("Damage");

        for (int i = 0; i < types.Count; i++)
        {
            switch (types[i])
            {
                case Type.Flame:
                    StartCoroutine(FlameDamage(10));
                    break;
                case Type.Thunder:
                    //雷攻撃の効果処理
                    break;
                case Type.Water:
                    break;
            }
        }
    }

    /// <summary>
    /// 魔法を打ち出す
    /// </summary>
    private void AttackMagic()
    {
        if (ThrowingObject != null && TargetObject != null)
        {
            // Ballオブジェクトの生成
            GameObject ball = Instantiate(ThrowingObject, ShotPosition.transform.position, Quaternion.identity);

            // 標的の座標
            Vector3 targetPosition = TargetObject.transform.position;

            // 射出角度
            float angle = ThrowingAngle;

            // 射出速度を算出
            Vector3 velocity = CalculateVelocity(ShotPosition.transform.position, targetPosition, angle);

            // 射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }
        else
        {
            throw new System.Exception("射出するオブジェクトまたは標的のオブジェクトが未設定です。");
        }
    }

    /// <summary>
    /// 標的に命中する射出速度の計算
    /// </summary>
    /// <param name="pointA">射出開始座標</param>
    /// <param name="pointB">標的の座標</param>
    /// <returns>射出速度</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // 垂直方向の距離y
        float y = pointA.y - pointB.y;

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }



    IEnumerator Attack()
    {
        AT = false;
        yield return new WaitForSeconds(Random.Range(8,15f));
        ani.SetTrigger("FlameAttack");
        AttackMagic();
        AS.PlayOneShot(sound);
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


    IEnumerator Dead()//HPが０死んだ
    {
        ani.SetBool("Die", true);
        die = true;
        yield return new WaitForSeconds(1f);
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(2f);
        while (Threshold < 1.1)
        {
            yield return new WaitForSeconds(0.1f);
            Threshold += Time.fixedDeltaTime;
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

