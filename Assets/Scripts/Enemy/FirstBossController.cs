using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    [SerializeField] private float HP = 100;

    [Tooltip("PlayerObject")]
    [SerializeField] private GameObject Player;

    [SerializeField, Tooltip("���񂾂Ƃ��̃p�[�e�B�N��")]
    private ParticleSystem particle;

    private Animator ani;

    private bool AT = true;//�U��

    private bool die = false;//����

    ///<summary>
    ///���ݏo��Enemy
    /// </summary>
    [SerializeField, Tooltip("Spawn������G�I�u�W�F�N�g")]
    private GameObject EnemyObject;

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

    [SerializeField, Tooltip("fiveAttack��MagicPrefab")]
    private GameObject FiveFlameMagic;

    [SerializeField, Tooltip("�GSpawn�ʒu")]
    private Transform[] EnemySpawnPos;

    [SerializeField, Tooltip("�U���Ŏg�p����5��shotPosition")]
    private GameObject[] FivePos;

    

    private Material material;

    private AudioSource AS;

    [SerializeField] private AudioClip sound;


    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.GetComponent<Animator>();

        GameObject child = transform.GetChild(1).gameObject;

        material = child.GetComponent<SkinnedMeshRenderer>().material;//�q�I�u�W�F�N�g�̃}�e���A�����Q�Ƃ���

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

    //�U����H��������ɎQ�Ƃ���C���^�[�t�F�C�X
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

    

    //�����_���ōU������R���[�`��
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

    //�U����i
    IEnumerator BigFlameAttack()
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

            rid.isKinematic = true;

            yield return new WaitForSeconds(8f);

            rid.isKinematic = false;

            ani.SetTrigger("FlameAttack");

            AS.PlayOneShot(sound);

            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

            AT = true;
        }
        
    }

    //�U����i
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


