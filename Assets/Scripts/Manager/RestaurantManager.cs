using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantManager : MonoBehaviour
{
    int score = 0;
    float time = 60.0f;
    float genTime = 1;
    [SerializeField] GameObject[] mouseObj;

    [Header("UI")]
    [SerializeField] Text timerText;
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] Text GameStartText;

    [Header("Result")]
    [SerializeField] GameObject Result;
    [SerializeField] GameObject[] ResultScoreImage;


    /// <summary>
    /// 어트랙션 고유코드
    /// </summary>
    public int attractionsCode;

    private void Start()
    {
        SceneController.Instance.FindObj();
        StartCoroutine("GameStart");
    }

    private void Update()
    {
        timerText.text = time.ToString("00");

        if (0 >= time)
        {
            OptionOn(Result);
        }

        if (score >= 100)
        {
            OptionOn(Result);
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
        InvokeRepeating("GenMouse", 0f, genTime);
        StartCoroutine("Timer");
    }

    public void addScore(int argNum)
    {
        score += argNum;
    }

    IEnumerator Timer()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    void GenMouse()
    {
        genTime = Random.Range(0, 20f);
        int num = Random.Range(0, 7);
        //Instantiate(obj[num], obj[num].transform.position, Quaternion.identity);
        mouseObj[num].SetActive(true);
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
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }
}
