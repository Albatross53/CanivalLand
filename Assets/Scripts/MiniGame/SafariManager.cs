using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafariManager : MonoBehaviour
{
    [SerializeField] AudioClip safariBack;

    float safariScore = 0;
    float time = 60.0f;
    float genTime = 2;
    [HideInInspector] public int goodNum = 0;

    [SerializeField] GameObject[] animal;
    [SerializeField] Transform[] animalPos;

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
    [SerializeField] int attractionsCode;
    static SafariManager g_safariManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_safariManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayBackSound(safariBack);

        SceneController.Instance.FindObj();
        goodNum = 0;
        GameStartPanel.SetActive(true);
        reaction.SetActive(false);
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timerText.text = time.ToString("00");
        reactionBar.fillAmount = safariScore / 300;

        if (0 >= time)
        {
            OptionOn(Result);
            GetResult();
        }

        /*
        if (safariScore >= 100)
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
        InvokeRepeating("GenAnimal", 0f, genTime);
        StartCoroutine("Timer");
    }

    void GetResult()
    {
        goodText.text = "X " + goodNum.ToString();
        if (safariScore <= 50)
        {
            ResultScoreImage[0].SetActive(false);
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (safariScore <= 100)
        {
            ResultScoreImage[1].SetActive(false);
            ResultScoreImage[2].SetActive(false);
        }
        else if (safariScore <= 200)
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
        safariScore += argNum;
    }

    IEnumerator Timer()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
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

        yield return new WaitForSeconds(1.0f);
        reaction.SetActive(false);
    }

    void GenAnimal()
    {
        int PosNum = Random.Range(0, 9);
        int AnimalNum = Random.Range(0, 3);
        Instantiate(animal[AnimalNum], animalPos[PosNum].position, Quaternion.identity);
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
        SceneController.Instance.LoadScene("Safari");
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

    public static SafariManager Instance
    {
        get { return g_safariManager; }
    }
}
