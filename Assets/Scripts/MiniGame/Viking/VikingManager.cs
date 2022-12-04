using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VikingManager : MonoBehaviour
{
    [SerializeField] AudioClip vikingBack;

    [SerializeField] int attractionsCode;
    float VikingScore = 0;
    float curTime = 60;

    [Header("UI")]
    [SerializeField] Text goodText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] Text GameStartText;

    [Header("TurnBtn")]
    [SerializeField] Button vikingBtn;
    [SerializeField] Sprite vikingBtnOn;
    [SerializeField] Sprite vikingBtnOff;

    [Header("Result")]
    [SerializeField] GameObject Result;
    [SerializeField] Text scoreText;
    [SerializeField] Text cardText;
    [SerializeField] Text rewardText;

    [Header("Reaction")]
    [SerializeField] GameObject reactionPrefab;
    [SerializeField] Transform reactionPos;
    [SerializeField] Sprite reactionGood;
    [SerializeField] Sprite reactionBad;
    [SerializeField] Image reactionBar;

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
        SoundManager.Instance.PlayBackSound(vikingBack);

        SceneController.Instance.FindObj();
        GameStartPanel.SetActive(true);
        Result.SetActive(false);
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timeText.text = curTime.ToString("00");
        reactionBar.fillAmount = VikingScore / 400;

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
        scoreText.text = "점수: " + Mathf.RoundToInt(VikingScore).ToString();
        cardText.text = "카드 영향 : x" +
            LuckcardManager.Instance.todayAffectingNum.ToString();
        rewardText.text = "획득골드 : " +
            (Mathf.RoundToInt(VikingScore) * LuckcardManager.Instance.todayAffectingNum).ToString();
        GameValueManager.Instance.IsMiniGameScore = Mathf.RoundToInt(VikingScore) * LuckcardManager.Instance.todayAffectingNum;
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
    public void AddScore(int argNum)
    {
        if(VikingScore < 400)
        {
            VikingScore += argNum;
        }
    }

    /// <summary>
    /// 왼쪽 위로 사라지는 리액션
    /// </summary>
    /// <param name="argBool">성공, 실패</param>
    public void GetReaction(bool argBool)
    {
        StartCoroutine("ReactionFade", argBool);
    }

    IEnumerator ReactionFade(bool argBool)
    {
        GameObject reaction = Instantiate(reactionPrefab, reactionPos);
        if (argBool)
        {
            reaction.GetComponent<Image>().sprite = reactionGood;
        }
        else
        {
            reaction.GetComponent<Image>().sprite = reactionBad;
        }
        yield return new WaitForSeconds(2);
        Destroy(reaction);
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
        ParkReputationManager.Instance.addReputation(5);
        AttractionPreferenceManager.Instance.AddPreference(attractionsCode - 1, 5);
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }

    public static VikingManager Instance
    {
        get { return g_vikingManager; }
    }
}
