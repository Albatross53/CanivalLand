using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlumRideManager : MonoBehaviour
{
    [SerializeField] AudioClip flumRideBack;
    [SerializeField] AudioClip goodSE;
    [SerializeField] AudioClip badSE;

    [HideInInspector] public int goodNum = 0;
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
    [SerializeField] GameObject[] ResultScoreImage;

    [Header("Reaction")]
    [SerializeField] GameObject reaction;
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
        goodNum = 0;
        GameStartPanel.SetActive(true);
        reaction.SetActive(false);
        NopePic = false;
        canPic = false;
        StartCoroutine("GameStart");
    }
    private void Update()
    {
        timerText.text = time.ToString("00");
        reactionBar.fillAmount = flumRideScore / 300;

        //timeover
        if (0 >= time)
        {
            OptionOn(Result);
            GetResult();
        }

        /*
        if(score >= 100)
        {
            OptionOn(Result);
        }
        */
    }

    public void Pic()
    {
        if (canPic && !NopePic)
        {
            goodNum++;
            SoundManager.Instance.PlayEffectSound(goodSE);
            addScore(10);
            GetReaction(true);
        }
        else
        {
            SoundManager.Instance.PlayEffectSound(badSE);
            addScore(-10);
            GetReaction(false);
        }
    }

    void GetResult()
    {
        goodText.text = "X " + goodNum.ToString();
        if (flumRideScore <= 50)
        {
            ResultScoreImage[0].SetActive(false);
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (flumRideScore <= 100)
        {
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (flumRideScore <= 200)
        {
            ResultScoreImage[2].SetActive(false);
        }
        else
        {
            return;
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
        InvokeRepeating("GenBoat", 0f, genTime);
        InvokeRepeating("GenObs", 0f, 3);
        StartCoroutine("Timer");
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

    public void addScore(int argNum)
    {
        flumRideScore += argNum;
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
