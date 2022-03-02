using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    private Text tutotext;

    private int page = 0;

    [SerializeField] private string[] manual;

    // Start is called before the first frame update
    void Start()
    {
        tutotext = GetComponent<Text>();
    }

    public void nextpage()
    {
        
        page += 1;

        switch(page)
        {
            case 0:
                tutotext.text = manual[page];
                break;
            case 1:
                tutotext.text = manual[page];
                break;
            case 2:
                tutotext.text = manual[page];
                break;
            case 3:
                tutotext.text = manual[page];
                break;
            case 4:
                tutotext.text = manual[page];
                break;
            case 5:
                tutotext.text = manual[page];
                break;
            case 6:
                tutotext.text = manual[page];
                break;
            case 7:
                tutotext.text = manual[page];
                break;
            case 8:
                tutotext.text = manual[page];
                break;
            case 9:
                tutotext.text = manual[page];
                break;
            case 10:
                tutotext.text = manual[page];
                break;
            case 11:
                tutotext.text = manual[page];
                break;
            case 12:
                tutotext.text = manual[page];
                break;
            case 13:
                tutotext.text = manual[page];
                break;
            case 14:
                tutotext.text = manual[page];
                break;


        }
    }

    public void backpage()
    {
        if (page >= 0)
        {
            page -= 1;

            switch (page)
            {
                case 0:
                    tutotext.text = manual[page];
                    break;
                case 1:
                    tutotext.text = manual[page];
                    break;
                case 2:
                    tutotext.text = manual[page];
                    break;
                case 3:
                    tutotext.text = manual[page];
                    break;
                case 4:
                    tutotext.text = manual[page];
                    break;
                case 5:
                    tutotext.text = manual[page];
                    break;
                case 6:
                    tutotext.text = manual[page];
                    break;
                case 7:
                    tutotext.text = manual[page];
                    break;
                case 8:
                    tutotext.text = manual[page];
                    break;
                case 9:
                    tutotext.text = manual[page];
                    break;
                case 10:
                    tutotext.text = manual[page];
                    break;
                case 11:
                    tutotext.text = manual[page];
                    break;
                case 12:
                    tutotext.text = manual[page];
                    break;
                case 13:
                    tutotext.text = manual[page];
                    break;
                case 14:
                    tutotext.text = manual[page];
                    break;
            }
        }
    }


}
