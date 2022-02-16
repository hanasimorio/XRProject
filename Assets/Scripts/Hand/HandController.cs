using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{

    [Tooltip("発射位置")]
    public Transform shotposition;
    [Tooltip("エフェクトポジション")]
    public Transform EffectPosition;

    [Tooltip("土魔法")]
    public GameObject ClayMagic;//土魔法
    [Tooltip("雷魔法")]
    public GameObject ThunderMagic;
    [Tooltip("星魔法")]
    public GameObject StarMagic;
    [Tooltip("火魔法")]
    public GameObject FlameMagic;
    [Tooltip("水魔法")]
    public GameObject WaterMagic;

    [Tooltip("土魔法エフェクト")]
    public GameObject ClayEffect;//土魔法
    [Tooltip("雷魔法エフェクト")]
    public GameObject ThundeEffect;
    [Tooltip("星魔法エフェクト")]
    public GameObject StarEffect;
    [Tooltip("火魔法エフェクト")]
    public GameObject FlameEffect;
    [Tooltip("水魔法エフェクト")]
    public GameObject WaterEffect;




    [Tooltip("火魔法の発射速度")]
    [SerializeField] private float FlameSpeed = 3;


    public InputActionReference AttackTrigger = null;

    private int HaveMagic = 0;


    //腕エフェクトに変数を持たせる
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
        if (HaveMagic <= 0)//右手に何も付与されていなかったら
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
        else if (other.gameObject.CompareTag("LeftHand"))//右に付与された状態で左手に接触したら
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

        IEnumerator ThunderShot()//1秒おきに雷魔法を3発放つ
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

