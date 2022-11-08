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
    [HideInInspector] public int goodNum = 0;

    [SerializeField] GameObject[] mouse;
    [SerializeField] bool[] mouseOpen;

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
        goodNum = 0;
        GameStartPanel.SetActive(true);
        reaction.SetActive(false);
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timerText.text = time.ToString("00");
        reactionBar.fillAmount = RestaurantScore / 300;

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
        goodText.text = "X " + goodNum.ToString();
        if (RestaurantScore <= 50)
        {
            ResultScoreImage[0].SetActive(false);
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (RestaurantScore <= 100)
        {
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (RestaurantScore <= 200)
        {
            ResultScoreImage[2].SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void addScore(int argNum)
    {
        RestaurantScore += argNum;
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

        yield return new WaitForSeconds(1.0f);
        reaction.SetActive(false);
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
        StartCoroutine("MouseLife");
    }

    IEnumerator MouseLife()
    {
        int num = Random.Range(0, mouse.Length);
        if (!mouseOpen[num])
        {
            mouse[num].SetActive(true);
            mouseOpen[num] = true;
            yield return new WaitForSeconds(6f);
            if (mouseOpen[num])
            {
                mouseOpen[num] = false;
                mouse[num].SetActive(false);
                SoundManager.Instance.PlayEffectSound(badSE);
                addScore(-10);
                GetReaction(false);
            }
        }
        yield return null;
    }

    public void CatchRat(int ratCode)
    {
        if (mouseOpen[ratCode])
        {
            mouseOpen[ratCode] = false;
            addScore(10);
            goodNum++;
            SoundManager.Instance.PlayEffectSound(goodSE);
            GetReaction(true);
            mouse[ratCode].SetActive(false);
        }
        return;
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
