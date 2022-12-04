using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantManager : MonoBehaviour
{
    [SerializeField] AudioClip restaurantBack;
    [SerializeField] AudioClip goodSE;
    [SerializeField] AudioClip badSE;

    float RestaurantScore = 0;
    float time = 60.0f;
    float genTime = 1;

    [SerializeField] Mouse[] mouse;

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


    /// <summary>
    /// 어트랙션 고유코드
    /// </summary>
    public int attractionsCode;


    static RestaurantManager g_restaurantManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_restaurantManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SoundManager.Instance.PlayBackSound(restaurantBack);
        SceneController.Instance.FindObj();
        GameStartPanel.SetActive(true);
        Result.SetActive(false);
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timerText.text = time.ToString("00");
        reactionBar.fillAmount = RestaurantScore / 400;

        if (0 >= time)
        {
            OptionOn(Result);
            GetResult();
            StopCoroutine("Timer");
        }

        /*
        if (RestaurantScore >= 100)
        {
            OptionOn(Result);
        }
        */
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
        InvokeRepeating("GenMouse", 0f, genTime);
        StartCoroutine("Timer");
    }

    void GetResult()
    {
        scoreText.text = "점수: " + Mathf.RoundToInt(RestaurantScore).ToString();
        cardText.text = "카드 영향 : x" +
            LuckcardManager.Instance.todayAffectingNum.ToString();
        rewardText.text = "획득골드 : " +
            (Mathf.RoundToInt(RestaurantScore) * LuckcardManager.Instance.todayAffectingNum).ToString();
        GameValueManager.Instance.IsMiniGameScore = Mathf.RoundToInt(RestaurantScore) * LuckcardManager.Instance.todayAffectingNum;
    }

    public void addScore(int argNum)
    {
        if(RestaurantScore < 400)
        {
            RestaurantScore += argNum;
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

    IEnumerator Timer()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    void GenMouse()
    {
        genTime = Random.Range(0, 10f);
        int num = Random.Range(0, mouse.Length);
        mouse[num].gen();

    }

    public void CatchRat()
    {
        addScore(20);
        SoundManager.Instance.PlayEffectSound(goodSE);
        GetReaction(true);
    }

    public void MissRat()
    {
        addScore(-20);
        SoundManager.Instance.PlayEffectSound(badSE);
        GetReaction(false);
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
        SceneController.Instance.LoadScene("Restaurant");
    }

    public void MiniGameExit()
    {
        SceneController.Instance.LoadScene("MainGame");
    }


    /// <summary>
    /// 어트랙션 종료
    /// </summary>
    public void EndGame()
    {
        GameValueManager.Instance.IsWorkimg = attractionsCode;
        ParkReputationManager.Instance.addReputation(5);
        AttractionPreferenceManager.Instance.AddPreference(attractionsCode - 1, 5);
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }

    public static RestaurantManager Instance
    {
        get { return g_restaurantManager; }
    }
}
