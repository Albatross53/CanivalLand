using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 상태 enum idle 평소, 옵션창 등 상태 stop, ending 게임엔딩
/// </summary>
enum GameState { idle = 0, stop, cut, dorm}

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip mainBack;

    /// <summary>
    /// 게임상태
    /// </summary>
    GameState m_gameState;

    /// <summary>
    /// 옵션창 오픈
    /// </summary>
    [HideInInspector] public bool m_optionOpen = false;

    /// <summary>
    /// 게임매니저 글로벌화
    /// </summary>
    static GameManager g_gameManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SoundManager.Instance.PlayBackSound(mainBack);

        //출근
        if (GameValueManager.IsDay)
        {
            QuestManager.Instance.StartGame();
            TimeManager.Instance.IsMin = 0;
            TimeManager.Instance.IsSec = 0;

            GameValueManager.Instance.ValueLoad();
            QuestManager.Instance.QuestLoad();
            LuckcardManager.Instance.LuckcardOpenLoad();
            ParkReputationManager.Instance.ReputationLoad();
            AttractionPreferenceManager.Instance.PreferenceLoad();
            GameValueManager.Instance.IsWorkimg = 0;

            QuestManager.Instance.NpcQuestCheck(3, 1);

            GameValueManager.IsDay = false;

        }
        else
        {
            //미니게임 후 메인씬 입장
            PlayerController.Instance.PlayerPosLoad();

            TimeManager.Instance.IsMin++;
            SceneController.Instance.FindObj();
            ChangeState(GameState.idle);
            AttractionPreferenceManager.Instance.PreferenceEffect(GameValueManager.Instance.IsWorkimg - 1);
            QuestManager.Instance.QuestCheck(GameValueManager.Instance.IsWorkimg);
            GameValueManager.Instance.IsWorkimg = 0;
        }

        SceneController.Instance.FindObj();
        NPCManager.Instance.NPCFriendshipLoad();
        TimeManager.Instance.StartGame();
    }

    private void Update()
    {
        if (m_optionOpen)
        {
            ChangeState(GameState.stop);
        }
        else
        {
            ChangeState(GameState.idle);
        }
    }

    /// <summary>
    /// 하루 종료
    /// </summary>
    public void WorkEnd()
    {
        GameValueManager.Instance.IsDate += 1;

        GameValueManager.Instance.ValueSave();
        QuestManager.Instance.QuestSave();
        LuckcardManager.Instance.LuckcardOpenSave();
        ParkReputationManager.Instance.ReputationSave();
        AttractionPreferenceManager.Instance.PreferenceSave();
        NPCManager.Instance.NPCFriendshipSave();

        string filePath = Application.persistentDataPath + "/PlayerData";
        SaveLoadManager.Instance.Save(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);

        SceneController.Instance.LoadScene("Dormitory");
    }

    /// <summary>
    /// 게임상태 바꾸기
    /// </summary>
    /// <param name="argState">바꾸는 상태</param>
    private void ChangeState(GameState argState)
    {
        StopCoroutine(m_gameState.ToString());
        m_gameState = argState;
        StartCoroutine(m_gameState.ToString());
    }

    IEnumerator idle()
    {
        Time.timeScale = 1;
        PlayerController.Instance.IsStop = false;
        yield return 0;
    }

    IEnumerator stop()
    {
        Time.timeScale = 0;
        PlayerController.Instance.IsStop = true;
        yield return 0;
    }

    IEnumerator cut()
    {
        PlayerController.Instance.IsStop = true;
        yield return 0;
    }

    IEnumerator dorm()
    {
        yield return 0;
    }

    public static GameManager Instance
    {
        get { return g_gameManager; }
    }

}
