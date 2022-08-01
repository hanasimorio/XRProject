using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawManager : MonoBehaviour
{

    //�ϐ���p��
    //SerializeField�������Inspector�E�B���h�E����Q�[���I�u�W�F�N�g��Prefab���w��ł��܂��B
    [SerializeField] GameObject LineObjectPrefab;
    [SerializeField] Transform HandAnchor;//position���擾����R���g���[���[�̈ʒu���
    public InputActionReference line = null;
    //���ݕ`�撆��LineObject;
    private GameObject CurrentLineObject = null;

    private Transform Pointer
    {
        get
        {
            return HandAnchor;
        }
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // ��������ǉ��R�[�h
        float value = line.action.ReadValue<float>();

        var pointer = Pointer;
        if (pointer == null)
        {
            Debug.Log("pointer not defiend");
            return;
        }

        //Oculus Touch�̐l�����w�̃g���K�[��������Ă����
        if (value >= 1)
        {
            if (CurrentLineObject == null)
            {
                //Prefab����LineObject�𐶐�
                CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            //�Q�[���I�u�W�F�N�g����LineRenderer�R���|�[�l���g���擾
            LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();

            //LineRenderer����Positions�̃T�C�Y���擾
            int NextPositionIndex = render.positionCount;

            //LineRenderer��Positions�̃T�C�Y�𑝂₷
            render.positionCount = NextPositionIndex + 1;

            //LineRenderer��Positions�Ɍ��݂̃R���g���[���[�̈ʒu����ǉ�
            render.SetPosition(NextPositionIndex, pointer.position);
        }
        else if (value <= 0)//�l�����w�̃g���K�[�𗣂����Ƃ�
        {
            if (CurrentLineObject != null)
            {
                //���ݕ`�撆�̐�����������null�ɂ��Ď��̐���`����悤�ɂ���B
                CurrentLineObject = null;
            }
        }
    }
}
