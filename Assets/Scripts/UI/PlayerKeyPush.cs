using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerKeyPush : MonoBehaviour
{
    private TextMeshPro PlayerTextOut;

    public UnityEvent Play;

    private bool Attention = false;

    private GameObject parent;

    private GameObject Trans;

    private TransManager TransScript;

    [SerializeField, Tooltip("線を描くGameObject")] private GameObject LinePos;

    private GameObject anotherHand;

    private AudioSource AS;

    [SerializeField] private AudioClip sound;


    // Start is called before the first frame update
    void Start()
    {
        PlayerTextOut = GameObject.FindGameObjectWithTag("PlayerTextOut").GetComponentInChildren<TextMeshPro>();
        LinePos.SetActive(false);
       
        Trans = GameObject.Find("TransManager");
        TransScript = Trans.GetComponent<TransManager>();
        AS = GetComponent<AudioSource>();


        if(this.name == "Hitosasi")
        {
            var hand = GameObject.FindGameObjectWithTag("LeftHand");
            anotherHand = hand.transform.GetChild(5).gameObject;
            anotherHand.SetActive(false);
        }
        else if(this.name == "LeftHitosasi")
        {
            var hand = GameObject.FindGameObjectWithTag("RightHand");
            anotherHand = hand.transform.GetChild(5).gameObject;
            anotherHand.SetActive(false);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponentInChildren<TextMeshPro>();


        if (key != null)
        {
            var KeyFeed = other.gameObject.GetComponent<KeyFeedback>();

            KeyFeed.keyhit = true;

            if (other.gameObject.GetComponent<KeyFeedback>().KeyAgain)
            {
                if(key.text == "Enter")
                {
                    if(PlayerTextOut.text == "PLAY")
                    {
                        PlayerTextOut.text = "Success!!";
                        parent = other.transform.root.gameObject;
                        LinePos.SetActive(true);//線をかけるようにする
                        anotherHand.SetActive(true);//もう片方の手も書けるように
                        TransScript.StartMove();
                        Destroy(parent);
                    }
                    else if(PlayerTextOut.text == "RETRY")
                    {
                        Play.Invoke();
                    }
                    else
                    {
                        PlayerTextOut.text = "Error";
                    }
                }
                else if(key.text == "<--")
                {
                    PlayerTextOut.text = PlayerTextOut.text.Substring(0, PlayerTextOut.text.Length - 1);
                }
                else if(!Attention)
                {
                    PlayerTextOut.text += key.text;
                    if(PlayerTextOut.text.Length > 10 && !Attention)
                    {
                        Attention = true;
                        PlayerTextOut.color = new Color(1, 1, 0, 1);
                        AS.PlayOneShot(sound);
                        PlayerTextOut.text = "異世界に行きたくない? 行きたいのならPLAYと打ち込んでEnterをクリック！";
                    }
                }
                else
                {
                    PlayerTextOut.color = new Color(1, 0, 0, 1);
                    Attention = false;
                    PlayerTextOut.text = "";
                    PlayerTextOut.text += key.text;
                }

            }
        }
    }
}
