using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronic: MonoBehaviour
{

    Rigidbody rb;
    //速度
    Vector3 velocity;
    //発射するときの初期位置
    // 加速度
    public Vector3 acceleration;
    // ターゲットをセットする
    private Transform target = null;
    // 着弾時間
    [SerializeField] private float period = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("Destroy", 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            acceleration = Vector3.zero;

            //ターゲットと自分自身の差
            var diff = target.position - transform.position;

            //加速度を求める
            acceleration += (diff - velocity * period) * 2f
                            / (period * period);


            //加速度が一定以上だと追尾を弱くする
            /*if (acceleration.magnitude > 100f)
            {
                acceleration = acceleration.normalized * 100f;
            }*/

            // 着弾時間を徐々に減らしていく
            period -= Time.deltaTime;

            // 速度の計算
            velocity += acceleration * Time.deltaTime;

            rb.MovePosition(transform.position + velocity * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            if (target == null)
            {
                target = other.gameObject.transform;
                rb.velocity = Vector3.zero;

            }

    }

    private void OnCollisionEnter(Collision collision)
    {
        var Enemy = collision.gameObject.GetComponent<IDamage>();
        if (Enemy != null)
        {
            List<Type> types = new List<Type> { Type.Thunder };
            Enemy.ApplyDamage(20f, types);
            Destroy(gameObject);
        }
    }


    private void Destroy()
    {
        Destroy(this.gameObject);
    }

}