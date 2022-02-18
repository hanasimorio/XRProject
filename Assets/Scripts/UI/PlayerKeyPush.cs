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

    // Start is called before the first frame update
    void Start()
    {
        PlayerTextOut = GameObject.FindGameObjectWithTag("PlayerTextOut").GetComponentInChildren<TextMeshPro>();
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
                        Play.Invoke();
                        PlayerTextOut.text = "Success!!";
                        parent = other.transform.root.gameObject;
                        Destroy(parent);
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
                        PlayerTextOut.text = "Do you want to another world? PushKeyCode: PLAY & EnterKeyPush";
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
