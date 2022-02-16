using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour,IPlayerDamage
{
    // Start is called before the first frame update

    [SerializeField, Tooltip("�v���C���[��HP")]
    private float HP = 200;

    [SerializeField, Tooltip("�_���[�W�󂯂��Ƃ��ɕ\������UI")]
    private GameObject DamageImage;

    [SerializeField, Tooltip("�]������Ƃ��̃G�t�F�N�g")]
    private GameObject Tenseieffect1;

    [SerializeField, Tooltip("�]������Ƃ��̃G�t�F�N�g")]
    private GameObject Tenseieffect2;

    [SerializeField, Tooltip("�G�t�F�N�g���o���|�W�V����")]
    private GameObject EffectPos;


    Image image;

    private float ff;

    void Start()
    {
        image = DamageImage.GetComponent<Image>();
        image.color = Color.clear;
        //DamageImage.transform.localScale = new Vector3(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void ApplyDamage(float damage, List<Attacks> attacks)
    {
        HP -= damage;
        if (HP <= 0)
        {

            //���S����
        }
        else if (HP <= 50)
        {
            this.image.color = new Color(0.5f, 0f, 0f, 1.0f);
        }

        else if (HP <= 100)
        {
            this.image.color = new Color(0.5f, 0f, 0f, 1.0f);
        }
        else if(HP <= 150)
        {
            this.image.color = new Color(0.5f, 0f, 0f, 1.0f);
        }

        for (int i = 0; i < attacks.Count; i++)
        {
            switch (attacks[i])
            {
                case Attacks.Flame:
                    
                    break;
                case Attacks.Slash:
                    
                    break;
            }
        }
    }

    public void TenseiEffect(float waittime)
    {
        StartCoroutine(SpawnEffect(waittime));
    }

    IEnumerator SpawnEffect(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        Instantiate(Tenseieffect1, EffectPos.transform.position, EffectPos.transform.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(Tenseieffect2, EffectPos.transform.position, EffectPos.transform.rotation);
    }

}
