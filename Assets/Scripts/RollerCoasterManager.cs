using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollerCoasterManager : MonoBehaviour
{
    [SerializeField] int attractionsCode;
    [SerializeField] float time = 60.0f;

    /// <summary>
    /// ����� ������Ʈ
    /// </summary>
    [SerializeField] GameObject[] waitObj;
    /// <summary>
    /// ����� ��������Ʈ
    /// </summary>
    [SerializeField] Sprite[] waitSprite;

    /// <summary>
    /// ���� ����� ��ȣ
    /// </summary>
    int nowNum = 0;
    /// <summary>
    /// ����
    /// </summary>
    int score = 0;
    /// <summary>
    /// ��ü ����� ����Ʈ
    /// </summary>
    List<int> waitList = new List<int>();
    /// <summary>
    /// ȭ�鿡 ������ �����
    /// </summary>
    Queue<int> waitQueue = new Queue<int>();

    [Header("UI")]
    [SerializeField] Text TimeText;
    [SerializeField] GameObject GameStartPanel;
    [SerializeField] Text GameStartText;

    [Header("Result")]
    [SerializeField] GameObject Result;
    [SerializeField] GameObject[] ResultScoreImage;


    void Start()
    {
        SceneController.Instance.FindObj();
        StartCoroutine("GameStart");
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = time.ToString("00");

        if (0 >= time)
        {
            OptionOn(Result);
        }

    }


    public void RideBtn()
    {
        if(waitQueue.Peek() == 0)
        {
            score++;
            waitQueue.Dequeue();
            WaitGen();
            nowNum++;
            WaitMatch();
        }
        else
        {
            score--;
        }
    }

    public void GetOutBtn()
    {
        if (waitQueue.Peek() != 0)
        {
            score++;
            waitQueue.Dequeue();
            WaitGen();
            nowNum++;
            WaitMatch();
        }
        else
        {
            score--;
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
        GameStartText.text = "���ӽ���";
        yield return new WaitForSeconds(1.0f);
        GameStartPanel.SetActive(false);
        StartCoroutine("Timer");

        for(int i = 0; i < 4; i++)
        {
            WaitGen();
        }
        WaitMatch();
    }

    void WaitGen()
    {
        int ranNum = Random.Range(0, 3);
        waitQueue.Enqueue(ranNum);
        waitList.Add(ranNum);   
    }

    void WaitMatch()
    {
        GetWaitSprite(nowNum, 0);
        GetWaitSprite(nowNum + 1, 1);
        GetWaitSprite(nowNum + 2, 2);
        GetWaitSprite(nowNum + 3, 3);
    }

    void GetWaitSprite(int num, int pos)
    {
        if (waitList[num] == 0)
        {
            waitObj[pos].GetComponent<SpriteRenderer>().sprite = waitSprite[0];
        }
        else if (waitList[num] == 1)
        {
            waitObj[pos].GetComponent<SpriteRenderer>().sprite = waitSprite[1];
        }
        else if (waitList[num] == 2)
        {
            waitObj[pos].GetComponent<SpriteRenderer>().sprite = waitSprite[2];
        }
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
        SceneController.Instance.LoadScene("RollerCoaster");
    }

    public void MiniGameExit()
    {
        SceneController.Instance.LoadScene("MainGame");
    }

    IEnumerator Timer()
    {
        time--;
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Timer");
    }

    /// <summary>
    /// ��Ʈ���� ����
    /// </summary>
    public void EndGame()
    {
        GameValueManager.Instance.IsWorkimg = attractionsCode;
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }
}
