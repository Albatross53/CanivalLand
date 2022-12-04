using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class FlumRideManager : MonoBehaviour
{
    [SerializeField] AudioClip flumRideBack;
    [SerializeField] AudioClip goodSE;
    [SerializeField] AudioClip badSE;

    [SerializeField] int attractionsCode;
    float time = 60.0f;
    float genTime = 5;
    [SerializeField] GameObject boat;
    [SerializeField] GameObject obs;
    float flumRideScore = 0;

    [Header("UI")]
    [SerializeField] Text goodText;
    [SerializeField] Text timerText;
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] Text GameStartText;

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

    public bool canPic { get; set; } = false;
    public bool NopePic { get; set; } = false;

    static FlumRideManager g_flumRideManager;
    private void Awake()
    {
        if(Instance == null)
        {
            g_flumRideManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SoundManager.Instance.PlayBackSound(flumRideBack);

        SceneController.Instance.FindObj();
        GameStartPanel.SetActive(true);
        NopePic = false;
        canPic = false;
        Result.SetActive(false);
        StartCoroutine("GameStart");
    }
    private void Update()
    {
        timerText.text = time.ToString("00");
        reactionBar.fillAmount = flumRideScore / 400;

        //timeover
        if (0 >= time)
        {
            OptionOn(Result);
            GetResult();
        }

    }

    public void Pic()
    {
        if (canPic && !NopePic)
        {
            SoundManager.Instance.PlayEffectSound(goodSE);
            addScore(20);
            GetReaction(true);
        }
        else
        {
            SoundManager.Instance.PlayEffectSound(badSE);
            addScore(-20);
            GetReaction(false);
        }
    }

    void GetResult()
    {
        scoreText.text = "점수: " + Mathf.RoundToInt(flumRideScore).ToString();
        cardText.text = "카드 영향 : x" + 
            LuckcardManager.Instance.todayAffectingNum.ToString();
        rewardText.text = "획득골드 : " +
            (Mathf.RoundToInt(flumRideScore) * LuckcardManager.Instance.todayAffectingNum).ToString();
        GameValueManager.Instance.IsMiniGameScore = Mathf.RoundToInt(flumRideScore) * LuckcardManager.Instance.todayAffectingNum;
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
        InvokeRepeating("GenBoat", 0f, genTime);
        InvokeRepeating("GenObs", 0f, 3);
        StartCoroutine("Timer");
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
        if(argBool)
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


    public void addScore(int argNum)
    {
        if(flumRideScore < 400)
        {
            flumRideScore += argNum;
        }
    }

    IEnumerator Timer()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    void GenBoat()
    {
        int num = Random.Range(0, 2);
        Instantiate(boat as GameObject);
    }

    void GenObs()
    {
        int num = Random.Range(0, 10);
        if(num < 3)
        {
            Instantiate(obs as GameObject);
        }
    }

    public void EndGame()
    {
        GameValueManager.Instance.IsWorkimg = attractionsCode;
        ParkReputationManager.Instance.addReputation(5);
        AttractionPreferenceManager.Instance.AddPreference(attractionsCode - 1, 5);
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }

    public void OptionOn(GameObject Option)
    {
        Option.SetActive(true);
        Time.timeScale = 0;
    }

    public void OptionOff(GameObject Option)
    {
        Option.SetActive(false);
        Time.timeScale = 1;
    }

    public void Replay()
    {
        SceneController.Instance.LoadScene("FlumRide");
    }

    public void MiniGameExit()
    {
        SceneController.Instance.LoadScene("MainGame");
    }


    public static FlumRideManager Instance
    {
        get { return g_flumRideManager; }
    }

}
