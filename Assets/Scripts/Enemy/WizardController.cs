using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour,IDamage
{
    [SerializeField] private float HP = 100;

    [Tooltip("PlayerObject")]
    private GameObject Player;

    [SerializeField, Tooltip("���񂾂Ƃ��̃p�[�e�B�N��")]
    private ParticleSystem particle;

    private Animator ani;

    private bool AT = true;

    private bool die = false;

    private AudioSource AS;

    [SerializeField] private AudioClip sound;

    /// <summary>
    /// �ˏo����I�u�W�F�N�g
    /// </summary>
    [SerializeField, Tooltip("�ˏo����I�u�W�F�N�g�������Ɋ��蓖�Ă�")]
    private GameObject ThrowingObject;

    /// <summary>
    /// �W�I�̃I�u�W�F�N�g
    /// </summary>
    [SerializeField, Tooltip("�W�I�̃I�u�W�F�N�g�������Ɋ��蓖�Ă�")]
    private GameObject TargetObject;

    /// <summary>
    /// �ˏo�p�x
    /// </summary>
    [SerializeField, Range(0F, 90F), Tooltip("�ˏo����p�x")]
    private float ThrowingAngle;

    /// <summary>
    /// �ˏo�|�C���g
    /// </summary>
    [SerializeField, Tooltip("�ˏo����|�W�V����")]
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
                    //���U���̌��ʏ���
                    break;
                case Type.Water:
                    break;
            }
        }
    }

    /// <summary>
    /// ���@��ł��o��
    /// </summary>
    private void AttackMagic()
    {
        if (ThrowingObject != null && TargetObject != null)
        {
            // Ball�I�u�W�F�N�g�̐���
            GameObject ball = Instantiate(ThrowingObject, ShotPosition.transform.position, Quaternion.identity);

            // �W�I�̍��W
            Vector3 targetPosition = TargetObject.transform.position;

            // �ˏo�p�x
            float angle = ThrowingAngle;

            // �ˏo���x���Z�o
            Vector3 velocity = CalculateVelocity(ShotPosition.transform.position, targetPosition, angle);

            // �ˏo
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }
        else
        {
            throw new System.Exception("�ˏo����I�u�W�F�N�g�܂��͕W�I�̃I�u�W�F�N�g�����ݒ�ł��B");
        }
    }

    /// <summary>
    /// �W�I�ɖ�������ˏo���x�̌v�Z
    /// </summary>
    /// <param name="pointA">�ˏo�J�n���W</param>
    /// <param name="pointB">�W�I�̍��W</param>
    /// <returns>�ˏo���x</returns>
    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // �ˏo�p�����W�A���ɕϊ�
        float rad = angle * Mathf.PI / 180;

        // ���������̋���x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));

        // ���������̋���y
        float y = pointA.y - pointB.y;

        // �Ε����˂̌����������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // �����𖞂����������Z�o�ł��Ȃ����Vector3.zero��Ԃ�
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
    IEnumerator FlameDamage(float Fdamage)//�Ζ��@��H�����
    {
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
        HP -= Fdamage;
        yield return new WaitForSeconds(2);
    }


    IEnumerator Dead()//HP���O����
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

