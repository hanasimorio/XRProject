using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int positionCount;
    private Camera mainCamera;

    private Vector3 linevec;
    private Vector3 currentvec;

    [Tooltip("�n�_����I�_�̋��e����")]
    public float OKStartAndEndDis = 2.0f;

    private List<Vector3> kado = new List<Vector3>();

    private int cornercount = 1;//�p�̌�

    [Tooltip("�y���@�̖��@�w�G�t�F�N�g")]
    public GameObject ClayMagicCircle;//�y���@
    [Tooltip("�����@�̖��@�w�G�t�F�N�g")]
    public GameObject ThunderMagicCircle;
    [Tooltip("�����@�̖��@�w�G�t�F�N�g")]
    public GameObject StarMagicCircle;
    [Tooltip("�Ζ��@�̖��@�w�G�t�F�N�g")]
    public GameObject FlameMagicCircle;
    [Tooltip("�����@�̖��@�w�G�t�F�N�g")]
    public GameObject WaterMagicCircle;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // ���C���̍��W�w����A���̃��C���I�u�W�F�N�g�̃��[�J�����W�n����ɂ���悤�ݒ��ύX
        // ���̏�ԂŃ��C���I�u�W�F�N�g���ړ��E��]������ƁA�`���ꂽ���C�������[���h��ԂɎ��c����邱�ƂȂ��A�ꏏ�Ɉړ��E��]
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����O��10m�A��]�̓J�����Ɠ����ɂȂ�悤�L�[�v������
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10;
        transform.rotation = mainCamera.transform.rotation;

        if (Input.GetMouseButton(0))
        {
            // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
            Vector3 pos = Input.mousePosition;
            pos.z = 10.0f;

            // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
            pos = mainCamera.ScreenToWorldPoint(pos);

            // ����ɂ�������[�J�����W�ɒ����B
            pos = transform.InverseTransformPoint(pos);

            // ����ꂽ���[�J�����W�����C�������_���[�ɒǉ�����
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);



            //�p�F��
            if (positionCount > 3)
            {
                var beforePositon = lineRenderer.GetPosition(positionCount - 2);//��O�̃x�N�g��
                var currentPosition = lineRenderer.GetPosition(positionCount - 1);//���݂̃x�N�g��

                currentvec = currentPosition - beforePositon;//��O���猻�݂̃x�N�g��

                //currentvec�����܂�ɂ��������ꍇ��������
                if (currentvec.sqrMagnitude >= 0.1f)
                {
                    linevec += currentvec; //�x�N�g�����a

                    //�x�N�g���̑��a��currentvec�̓���
                    var dot = Vector3.Dot(linevec.normalized, currentvec.normalized);

                    //���ς�corner��F������
                    if (dot < 0.8f)
                    {�@�@//�x�N�g���̑��a���������ꍇ�A�p�ƔF�����Ȃ�
                        if (linevec.sqrMagnitude > 1f)
                        {
                            linevec = currentvec;
                            Debug.Log("Corner");
                            kado.Add(currentPosition);
                            cornercount += 1;

                        }
                    }

                }

            }


        }






        //�}�`�F���i���@�F���j
        if (!(Input.GetMouseButton(0)))
        {
            //�`�������@���l�p�`���ǂ���
            if (SquareJudge())
            {
                Debug.Log("Square!!");
                Reset();
            }
            else if (Thunder())//
            {
                Debug.Log("Thunder");
                Reset();
            }
            else if (Star())
            {
                Debug.Log("Star");
                Reset();
            }
            else if (Flame())
            {
                Debug.Log("Flame");
                Reset();
            }
            else if (Water())
            {
                Debug.Log("Water");
                Reset();
            }
            else Reset();

        }
    }


    //Reset
    void Reset()
    {
        cornercount = 1;
        kado.Clear();
        positionCount = 0;
        linevec = currentvec;
    }

    //�l�p�`����i�y���@�j
    bool SquareJudge()
    {�@�@//�p�̌�
        if (cornercount == 4)
        {
            kado.Add(lineRenderer.GetPosition(0));
            var c1 = kado[3]; //startpoint
            var c2 = kado[0]; //firstcorner
            var c3 = kado[1]; //secondcorner
            var c4 = kado[2]; //thirdcorner
            /*
            Debug.Log(c1);
            Debug.Log(c2);
            Debug.Log(c3);
            Debug.Log(c4);*/

            var end = lineRenderer.GetPosition(positionCount - 1);

            //�n�_�ƏI�_�̈ʒu����
            if (Vector3.Distance(c1, end) < OKStartAndEndDis)
            {�@�@�@//���e����p�x
                const float ErrorAngle = 20;
                var ang0 = Vector3.Angle(c1 - c2, c1 - c4);
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - c4);
                var ang3 = Vector3.Angle(c4 - c1, c4 - c3);

                /*  Debug.Log(ang0);
                  Debug.Log(ang1);
                  Debug.Log(ang2);
                  Debug.Log(ang3);*/

                //corner�̊p�x���Z�o
                if (Mathf.Abs(ang0 - 90) < ErrorAngle &&
                    Mathf.Abs(ang1 - 90) < ErrorAngle &&
                    Mathf.Abs(ang2 - 90) < ErrorAngle &&
                    Mathf.Abs(ang3 - 90) < ErrorAngle)
                {
                    //�}�`�̒��S�_������o��
                    var center = (c1 + c2 + c3 + c4) * 0.25f;

                    Debug.Log(center);

                    Instantiate(ClayMagicCircle, center, Quaternion.identity);

                    return true;
                }
                else
                {
                    Debug.Log("Square:�p�x�s��");
                    return false;
                }
            }
            else
            {
                Debug.Log("Square:�n�_�ƏI�_�����ꂷ���Ă���");
                return false;
            }
        }
        else return false;
    }

    bool Thunder()
    {    //�p�̌�
        if (cornercount == 3)
        {
            kado.Add(lineRenderer.GetPosition(0));
            var c1 = kado[2];
            var c2 = kado[0];
            var c3 = kado[1];

            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(c1, end) > 3)
            {
                const float ErrorAngle = 20;
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - end);



                if (Mathf.Abs(ang1 - 60) < ErrorAngle &&
                    Mathf.Abs(ang2 - 60) < ErrorAngle)
                {
                    var center = (c2 + c3) * 0.5f;
                    Instantiate(ThunderMagicCircle, center, Quaternion.identity);
                    return true;
                }
                else
                {
                    Debug.Log("Thunder: �p�x�s��");
                    return false;
                }
            }
            else
            {
                Debug.Log("Thunder:�n�_�ƏI�_���߂�����");
                return false;
            }
        }
        else return false;

    }

    //�����@�F��
    bool Star()
    {    //�p�̌�
        if (cornercount == 5)
        {
            kado.Add(lineRenderer.GetPosition(0));

            var c1 = kado[4];
            var c2 = kado[0];
            var c3 = kado[1];
            var c4 = kado[2];
            var c5 = kado[3];

            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(c1, end) < OKStartAndEndDis)
            {
                const float ErrorAngle = 20;
                var ang0 = Vector3.Angle(c1 - c2, c1 - c5);
                var ang1 = Vector3.Angle(c2 - c1, c2 - c3);
                var ang2 = Vector3.Angle(c3 - c2, c3 - c4);
                var ang3 = Vector3.Angle(c4 - c3, c4 - c5);
                var ang4 = Vector3.Angle(c5 - c1, c5 - c4);

                if (Mathf.Abs(ang0 - 36) < ErrorAngle &&
                   Mathf.Abs(ang1 - 36) < ErrorAngle &&
                   Mathf.Abs(ang2 - 36) < ErrorAngle &&
                   Mathf.Abs(ang3 - 36) < ErrorAngle &&
                   Mathf.Abs(ang4 - 36) < ErrorAngle)
                {
                    var center = (c1 + c2 + c3 + c4 + c5) * 0.2f;
                    Instantiate(StarMagicCircle, center, Quaternion.identity);
                    return true;
                }
                else
                {
                    Debug.Log("Star:�p�x�s��");
                    return false;
                }
            }
            else
            {
                Debug.Log("Star:�n�_�ƏI�_�����ꂷ���Ă���");
                return false;
            }
        }
        else return false;
    }

    //�����@
    bool Flame()
    {
        if (cornercount > 6)
        {
            var start = lineRenderer.GetPosition(0);
            var end = lineRenderer.GetPosition(positionCount - 1);

            if (Vector3.Distance(start, end) < OKStartAndEndDis)
            {
                Instantiate(FlameMagicCircle, start, Quaternion.identity);
                return true;
            }
            else
            {
                Debug.Log("Flame:�n�_�ƏI�_�����ꂷ���Ă���");
                return false;
            }

        }
        else return false;
    }

    //�~�F���i�����@�j
    bool Water()
    {
        if (positionCount > 10)
        {
            var Sum = Vector3.zero;
            var start = lineRenderer.GetPosition(0);
            var end = lineRenderer.GetPosition(positionCount - 1);
            var AveDistance = 0f;
            var nextAveDistnce = 0f;

            if (Vector3.Distance(start, end) < OKStartAndEndDis && cornercount < 2)
            {
                //�K���ȊԊu�̓_�𑫂��A���S�_������o���B
                for (int i = 0; i < positionCount - 5; i += 5)
                {
                    Sum += lineRenderer.GetPosition(i);
                }

                var Center = Sum / (positionCount / 5);

                //�e�_�̒��S�_����̋������r���A���e�͈͓������肷��
                for (int j = 0; j < positionCount - 5; j += 5)
                {
                    AveDistance = Vector3.Distance(Center, lineRenderer.GetPosition(j));
                    if (j < 1)
                    {
                        nextAveDistnce = AveDistance;
                        continue;
                    }

                    if (Mathf.Abs(AveDistance - nextAveDistnce) < 0.6)
                    {
                        nextAveDistnce = AveDistance;
                    }
                    else
                    {
                        Debug.Log("Circle:�����Ⴂ");
                        return false;
                    }



                }

                Instantiate(WaterMagicCircle, Center, Quaternion.identity);
                return true;
            }

            Debug.Log("Circle:�n�_�ƏI�_�����ꂷ���Ă���������͊p�����݂��Ă���");
            return false;
        }

        return false;
    }
}