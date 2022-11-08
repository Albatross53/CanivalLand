using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [SerializeField] Text introText;
    [SerializeField][TextArea] string[] aText;
    [SerializeField] Sprite BadGuys;
    [SerializeField] GameObject background;

    [SerializeField] GameObject PayOrNot;

    [SerializeField] AudioClip back;

    int textNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.FindObj();
        SoundManager.Instance.PlayBackSound(back);

        SetText();
    }

    void SetText()
    {
        introText.text = aText[textNum];
    }

    public void NextText()
    {
        textNum++;

        if(textNum < aText.Length)
        {
            if(textNum == 4)
            {
                background.GetComponent<SpriteRenderer>().sprite = BadGuys;
            }
            SetText();
        }
        else
        {
            SkipBtn();
        }
    }

    public void SkipBtn()
    {
        PayOrNot.SetActive(true);
    }

    public void NotPayBtn()
    {
        SceneController.Instance.LoadScene("Ending");
    }

    public void WillPayBtn()
    {
        SceneController.Instance.LoadScene("Dormitory");
    }
}
