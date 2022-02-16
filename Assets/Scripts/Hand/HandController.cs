using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{

    [Tooltip("���ˈʒu")]
    public Transform shotposition;
    [Tooltip("�G�t�F�N�g�|�W�V����")]
    public Transform EffectPosition;

    [Tooltip("�y���@")]
    public GameObject ClayMagic;//�y���@
    [Tooltip("�����@")]
    public GameObject ThunderMagic;
    [Tooltip("�����@")]
    public GameObject StarMagic;
    [Tooltip("�Ζ��@")]
    public GameObject FlameMagic;
    [Tooltip("�����@")]
    public GameObject WaterMagic;

    [Tooltip("�y���@�G�t�F�N�g")]
    public GameObject ClayEffect;//�y���@
    [Tooltip("�����@�G�t�F�N�g")]
    public GameObject ThundeEffect;
    [Tooltip("�����@�G�t�F�N�g")]
    public GameObject StarEffect;
    [Tooltip("�Ζ��@�G�t�F�N�g")]
    public GameObject FlameEffect;
    [Tooltip("�����@�G�t�F�N�g")]
    public GameObject WaterEffect;




    [Tooltip("�Ζ��@�̔��ˑ��x")]
    [SerializeField] private float FlameSpeed = 3;


    public InputActionReference AttackTrigger = null;

    private int HaveMagic = 0;


    //�r�G�t�F�N�g�ɕϐ�����������
    private GameObject EfParticle = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float value = AttackTrigger.action.ReadValue<float>();

        if(value >= 1)
        {
            if(HaveMagic > 0)
            {
                switch(HaveMagic)
                {
                    case 1://singleFlame
                        HaveMagic = 0;
                        Instantiate(FlameMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
                        Destroy(EfParticle);
                        break;

                    case 2://DoubleFlame
                        HaveMagic = 0;
                        var DFlame = Instantiate(FlameMagic, gameObject.transform.position, Quaternion.identity);
                        DFlame.transform.localScale = DFlame.transform.localScale * 3;
                        DFlame.GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
                        Destroy(EfParticle);
                        break;

                    case 3://SingleClay
                        HaveMagic = 0;
                        Instantiate(ClayMagic, shotposition.position,shotposition.rotation);
                        Destroy(EfParticle);
                        break;

                    case 6://DoubelClay
                        HaveMagic = 0;
                        var DClay = Instantiate(ClayMagic, shotposition.position, shotposition.rotation);
                        DClay.transform.localScale = DClay.transform.localScale * 3;
                        Destroy(EfParticle);
                        break;

                    case 7://SingleThunder
                        HaveMagic = 0;
                        StartCoroutine(ThunderShot());
                        Destroy(EfParticle);
                        break;

                    case 14://DoubleThunder
                        HaveMagic = 0;
                        StartCoroutine(DoubleThunderShot());
                        Destroy(EfParticle);
                        break;

                    case 15://SingleStar
                        HaveMagic = 0;
                        Instantiate(StarMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * 200);                        
                        Destroy(EfParticle);
                        break;

                    case 30://DoubleStar
                        HaveMagic = 0;
                        var DStar = Instantiate(StarMagic, gameObject.transform.position, Quaternion.identity);
                        DStar.transform.localScale = DStar.transform.localScale * 3;
                        DStar.GetComponent<Rigidbody>().AddForce(shotposition.forward * 200);
                        Destroy(EfParticle);
                        break;

                    case 31://SingleWater
                        HaveMagic = 0;
                        Instantiate(WaterMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed); 
                        Destroy(EfParticle);
                        break;

                    case 62://DoubleWater

                        break;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (HaveMagic <= 0)//�E��ɉ����t�^����Ă��Ȃ�������
        {
            if (other.gameObject.CompareTag("Flame"))
            {
                EfParticle = Instantiate(FlameEffect, EffectPosition.position, EffectPosition.rotation);
                EfParticle.transform.SetParent(this.gameObject.transform);
                HaveMagic += 1;
            }
            else if (other.gameObject.CompareTag("Clay"))
            {
                EfParticle = Instantiate(ClayEffect, EffectPosition.position, EffectPosition.rotation);
                EfParticle.transform.SetParent(this.gameObject.transform);
                HaveMagic += 3;
            }
            else if (other.gameObject.CompareTag("Thunder"))
            {
                EfParticle = Instantiate(ThundeEffect, EffectPosition.position, EffectPosition.rotation);
                EfParticle.transform.SetParent(this.gameObject.transform);
                HaveMagic += 7;
            }
            else if (other.gameObject.CompareTag("Star"))
            {
                EfParticle = Instantiate(StarEffect, EffectPosition.position, EffectPosition.rotation);
                EfParticle.transform.SetParent(this.gameObject.transform);
                HaveMagic += 15;
            }
            else if (other.gameObject.CompareTag("Water"))
            {
                EfParticle = Instantiate(WaterEffect, EffectPosition.position, EffectPosition.rotation);
                EfParticle.transform.SetParent(this.gameObject.transform);
                HaveMagic += 31;
            }

        }
        else if (other.gameObject.CompareTag("LeftHand"))//�E�ɕt�^���ꂽ��Ԃō���ɐڐG������
        {
            var Left = other.gameObject.GetComponent<HandController>();
            if (HaveMagic == Left.HaveMagic)
            {
                switch (Left.HaveMagic)
                {
                    case 1:
                        EfParticle = Instantiate(FlameEffect, EffectPosition.position, EffectPosition.rotation);
                        EfParticle.transform.SetParent(this.gameObject.transform);
                        HaveMagic += 1;
                        Destroy(Left.EfParticle);
                        break;
                    case 3:
                        EfParticle = Instantiate(ClayEffect, EffectPosition.position, EffectPosition.rotation);
                        EfParticle.transform.SetParent(this.gameObject.transform);
                        HaveMagic += 3;
                        Destroy(Left.EfParticle);
                        break;
                    case 7:
                        EfParticle = Instantiate(ThundeEffect, EffectPosition.position, EffectPosition.rotation);
                        EfParticle.transform.SetParent(this.gameObject.transform);
                        HaveMagic += 7;
                        Destroy(Left.EfParticle);
                        break;
                    case 15:
                        EfParticle = Instantiate(StarEffect, EffectPosition.position, EffectPosition.rotation);
                        EfParticle.transform.SetParent(this.gameObject.transform);
                        HaveMagic += 15;
                        Destroy(Left.EfParticle);
                        break;
                    case 31:
                        EfParticle = Instantiate(WaterEffect, EffectPosition.position, EffectPosition.rotation);
                        EfParticle.transform.SetParent(this.gameObject.transform);
                        HaveMagic += 31;
                        Destroy(Left.EfParticle);
                        break;

                }
            }

        }
    }

        IEnumerator ThunderShot()//1�b�����ɗ����@��3������
        {
            Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
            yield return new WaitForSeconds(1f);
            Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
            yield return new WaitForSeconds(1f);
            Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
            Destroy(EfParticle);
        }

    IEnumerator DoubleThunderShot()
    {
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        yield return new WaitForSeconds(0.5f);
        Instantiate(ThunderMagic, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(shotposition.forward * FlameSpeed);
        Destroy(EfParticle);
    }
}

