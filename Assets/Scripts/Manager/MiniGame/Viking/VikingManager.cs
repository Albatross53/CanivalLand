using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingManager : MonoBehaviour
{
    [SerializeField] int attractionsCode;
    float reactionScore = 0;
    float curTime = 60;

    [Header("UI")]
    [SerializeField] Text timeText;
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] Text GameStartText;

    [Header("TurnBtn")]
    [SerializeField] Button vikingBtn;
    [SerializeField] Sprite vikingBtnOn;
    [SerializeField] Sprite vikingBtnOff;

    [Header("Reaction")]
    [SerializeField] GameObject reaction;
    [SerializeField] Sprite reactionGood;
    [SerializeField] Sprite reactionBad;
    [SerializeField] Image reactionBar;

    [Header("Result")]
    [SerializeField] GameObject Result;
    [SerializeField] GameObject[] ResultScoreImage;

    static VikingManager g_vikingManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_vikingManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SceneController.Instance.FindObj();
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timeText.text = curTime.ToString("00");
        reactionBar.fillAmount = reactionScore / 300;

        //timeover
        if(curTime <= 0)
        {
            Viking.Instance.VikingTurn(false);
            Result.SetActive(true);
            GetResult();
            StopCoroutine("Timer");
            curTime = 0;
        }

    }

    IEnumerator GameStart()
    {
        GameStartText.text = "3";
        yield return new WaitForSeconds(1.0f);
        GameStartText.text = "2";
        yield return new WaitForSeconds(1.0f);
        GameStartText.text = "1";
        yield return new WaitForSeconds(1.0f);
        GameStartText.text = "게임시작";
        yield return new WaitForSeconds(1.0f);
        GameStartPanel.SetActive(false);
        Viking.Instance.VikingTurn(true);
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        curTime--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    void GetResult()
    {
        if (reactionScore <= 50)
        {
            ResultScoreImage[0].SetActive(false);
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (reactionScore <= 100)
        {
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (reactionScore <= 200)
        {
            ResultScoreImage[2].SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void BtnActive(bool argBool)
    {
        vikingBtn.interactable = argBool;

        if(argBool == true)
        {
            vikingBtn.GetComponent<Image>().sprite = vikingBtnOn;
        }
        else
        {
            vikingBtn.GetComponent<Image>().sprite = vikingBtnOff;
        }
    }

    /// <summary>
    /// 점수추가
    /// </summary>
    public void AddScore()
    {
        reactionScore += 10;
    }

    public void GetReaction(bool argBool)
    {
        StartCoroutine("ReactionActive", argBool);
    }

    /// <summary>
    /// 리액션 보이기
    /// </summary>
    /// <param name="argBool">리액션확인</param>
    /// <returns></returns>
    IEnumerator ReactionActive(bool argBool)
    {
        reaction.SetActive(true);
        if (argBool)
        {
            reaction.GetComponent<Image>().sprite = reactionGood;
        }
        else
        {
            reaction.GetComponent<Image>().sprite = reactionBad;
        }

        yield return new WaitForSeconds(2.0f);
        reaction.SetActive(false);
    }

    public void OptionOn(GameObject Option)
    {
        Option.SetActive(true);
        StopCoroutine("Timer");
        Viking.Instance.VikingTurn(false);
    }

    public void OptionOff(GameObject Option)
    {
        Option.SetActive(false);
        StartCoroutine("Timer");
        Viking.Instance.VikingTurn(true);
    }

    public void Replay()
    {
        SceneController.Instance.LoadScene("Viking");
    }

    public void MiniGameExit()
    {
        SceneController.Instance.LoadScene("MainGame");
    }



    public void EndGame()
    {  
        GameValueManager.Instance.IsWorkimg = attractionsCode;
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }

    public static VikingManager Instance
    {
        get { return g_vikingManager; }
    }
}
