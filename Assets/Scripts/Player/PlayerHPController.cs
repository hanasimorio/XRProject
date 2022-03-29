using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHPController : MonoBehaviour,IPlayerDamage
{
    // Start is called before the first frame update

    [SerializeField, Tooltip("プレイヤーのHP")]
    private float HP = 200;

    private float starthp;

    [SerializeField, Tooltip("ダメージ受けたときに表示するUI")]
    private GameObject DamageImage;

    //[SerializeField, Tooltip("転生するときのエフェクト")]
    //private GameObject Tenseieffect1;

    //[SerializeField, Tooltip("転生するときのエフェクト")]
   // private GameObject Tenseieffect2;

    [SerializeField, Tooltip("エフェクトを出すポジション")]
    private GameObject EffectPos;


    Image image;

    [SerializeField, Tooltip("DeadUI")]
    private Image deadimage;

    private float ff;

    void Start()
    {
        image = DamageImage.GetComponent<Image>();
        image.color = Color.clear;
        deadimage.color = Color.clear;
        starthp = HP;
        //DamageImage.transform.localScale = new Vector3(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void ApplyDamage(float damage, List<Attacks> attacks)
    {
        HP -= damage;

        for (int i = 0; i < attacks.Count; i++)
        {
            switch (attacks[i])
            {
                case Attacks.Flame:

                    break;
                case Attacks.Slash:

                    break;
                case Attacks.Water:
                    HP += 50;
                    if (HP > starthp)
                        HP = starthp;
                    break;


            }
        }


        if (HP <= 0)
        {

            //死亡処理
            StartCoroutine(Dead());

        }
        else if (HP <= 50)
        {
            this.image.color = new Color(0.8f, 0f, 0f, 1.0f);
        }

        else if (HP <= 100)
        {
            this.image.color = new Color(0.5f, 0f, 0f, 1.0f);
        }
        else if(HP <= 150)
        {
            this.image.color = new Color(0.2f, 0f, 0f, 1.0f);
        }

        
    }


    IEnumerator Dead()
    {
        for (float i = 0; i < 1.0f; i += 0.05f)
        {
            deadimage.color = new Color(0f, 0f, 0f, i);
        }
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("TitleScene");
    }

   /* public void TenseiEffect(float waittime)
    {
        StartCoroutine(SpawnEffect(waittime));
    }

    IEnumerator SpawnEffect(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        Instantiate(Tenseieffect1, EffectPos.transform.position, EffectPos.transform.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(Tenseieffect2, EffectPos.transform.position, EffectPos.transform.rotation);
    }*/





}
