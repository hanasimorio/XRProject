using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem particle;
    void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particle.isStopped) //�p�[�e�B�N�����I������������
        {
            Destroy(this.gameObject);//�p�[�e�B�N���p�Q�[���I�u�W�F�N�g���폜
        }
    }

}
