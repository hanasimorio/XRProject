using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    [SerializeField] private float HP = 100;

    [Tooltip("PlayerObject")]
    [SerializeField] private GameObject Player;

    [SerializeField, Tooltip("死んだときのパーティクル")]
    private ParticleSystem particle;

    private Animator ani;

    private bool AT = true;//攻撃

    private bool die = false;//死んだ

    ///<summary>
    ///生み出すEnemy
    /// </summary>
    [SerializeField, Tooltip("Spawnさせる敵オブジェクト")]
    private GameObject EnemyObject;

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

    [SerializeField, Tooltip("fiveAttackのMagicPrefab")]
    private GameObject FiveFlameMagic;

    [SerializeField, Tooltip("敵Spawn位置")]
    private Transform[] EnemySpawnPos;

    [SerializeField, Tooltip("攻撃で使用する5個のshotPosition")]
    private GameObject[] FivePos;

    

    private Material material;

    private AudioSource AS;

    [SerializeField] private AudioClip sound;


    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();

        GameObject child = transform.GetChild(1).gameObject;

        material = child.GetComponent<SkinnedMeshRenderer>().material;//子オブジェクトのマテリアルを参照する

        AS.GetComponent<AudioSource>();

        //GameManager.instance.SpawnCountUp();
        //StartCoroutine(TestSpawner());

        for(int i = 0; i < 5; i++)
        {
            FivePos[i].transform.LookAt(Player.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && !die)
        {
            StartCoroutine(Dead());
            //GameManager.instance.EnemyDead();
        }
        else if (AT)
        {
            StartCoroutine(Attack());
        }

    }

    //攻撃を食らった時に参照するインターフェイス
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

    

    //ランダムで攻撃するコルーチン
    IEnumerator Attack()
    {
        AT = false;
        yield return new WaitForSeconds(Random.Range(8, 15f));
        int RandomAttack = Random.Range(0,3);
        switch (RandomAttack)
        {
            case 0:
                StartCoroutine(BigFlameAttack());
                break;
            case 1:
                StartCoroutine(EnemySpawn());
                break;
            case 2:
                StartCoroutine(FiveAttack());
                break;
        }

    }

    //攻撃手段
    IEnumerator BigFlameAttack()
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

            rid.isKinematic = true;

            yield return new WaitForSeconds(8f);

            rid.isKinematic = false;

            ani.SetTrigger("FlameAttack");

            AS.PlayOneShot(sound);

            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

            AT = true;
        }
        
    }

    //攻撃手段
    IEnumerator EnemySpawn()
    {
        var PosNum = Random.Range(0, 2);
        ani.SetTrigger("EnemySpawnAttack");
        Instantiate(EnemyObject, EnemySpawnPos[PosNum].position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        PosNum = Random.Range(0, 2);
        Instantiate(EnemyObject, EnemySpawnPos[PosNum].position, Quaternion.identity);
        AT = true;
    }

    IEnumerator FiveAttack()
    {
        for(int i = 0; i < 5; i++)
        {
            var t = Instantiate(FiveFlameMagic, FivePos[i].transform.position, Quaternion.identity);
            AS.PlayOneShot(sound);
            yield return new WaitForSeconds(2f);
            t.GetComponent<Rigidbody>().velocity = FivePos[i].transform.forward.normalized * 30;
        }
        
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
        yield return new WaitForSeconds(2f);
        material.SetFloat("_Threshold", 0.003f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.006f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.09f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.12f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.15f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.2f);
        yield return new WaitForSeconds(0.1f);
        material.SetFloat("_Threshold", 0.3f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator TestSpawner()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

}


