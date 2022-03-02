using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electronic: MonoBehaviour
{

    Rigidbody rb;
    //���x
    Vector3 velocity;
    //���˂���Ƃ��̏����ʒu
    // �����x
    public Vector3 acceleration;
    // �^�[�Q�b�g���Z�b�g����
    private Transform target = null;
    // ���e����
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

            //�^�[�Q�b�g�Ǝ������g�̍�
            var diff = target.position - transform.position;

            //�����x�����߂�
            acceleration += (diff - velocity * period) * 2f
                            / (period * period);


            //�����x�����ȏゾ�ƒǔ����キ����
            /*if (acceleration.magnitude > 100f)
            {
                acceleration = acceleration.normalized * 100f;
            }*/

            // ���e���Ԃ����X�Ɍ��炵�Ă���
            period -= Time.deltaTime;

            // ���x�̌v�Z
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