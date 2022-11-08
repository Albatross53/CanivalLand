using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 직원 호감도 저장및 로드 NpcController불러오기 직원호감도 옵션창 표시
/// </summary>
public class NPCManager : MonoBehaviour
{
    /// <summary>
    /// 메인씬 내 NpcController
    /// </summary>
    public NpcController[] npcList;

    /// <summary>
    /// 직원 처음 대면 여부
    /// </summary>
    public List<bool> npcFirstTalkList = new List<bool>();

    /// <summary>
    /// 오늘 대화 여부
    /// </summary>
    List<bool> npcFirstTalkToDay;

    /// <summary>
    /// 데이터 로드용 직원 호감도
    /// </summary>
    List<float> npcFriendShipList = new List<float>();

    /// <summary>
    /// 인게임 직원 호감도
    /// </summary>
    //List<float> AddNpcFriendShipList = new List<float>();

    /// <summary>
    /// 옵션창 직원 초상화 이미지
    /// </summary>
    [SerializeField] Image[] npcIcon;
    /// <summary>
    /// 옵션창 직원 호감도 표시 이미지
    /// </summary>
    [SerializeField] Image[] friendshipSlider;
    /// <summary>
    /// 직원 이름 텍스트
    /// </summary>
    [SerializeField] Text[] npcNameText;
    /// <summary>
    /// 직원 설명 텍스트
    /// </summary>
    [SerializeField] Text[] npcDisText;
    
    /// <summary>
    /// 글로벌화
    /// </summary>
    static NPCManager g_npcManager;

    private void Awake()
    {
        //싱글톤
        if(Instance == null)
        {
            g_npcManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 직원 호감도 옵션창 표시
    /// </summary>
    public void Option()
    {
        for(int i = 0; i < npcList.Length; i++)
        {
            if(npcFirstTalkList[i] == true)
            {
                npcIcon[i].sprite = npcList[i].npcData.NpcIcon;
                friendshipSlider[i].fillAmount = npcFriendShipList[i] / 100;
                npcNameText[i].text = npcList[i].npcData.NpcName;
                npcDisText[i].text = npcList[i].npcData.NpcDis;
            }
            else
            {
                npcIcon[i].sprite = null;
                friendshipSlider[i].fillAmount = 0;
                npcNameText[i].text = "?";
                npcDisText[i].text = "?";
            }
        }
    }

    /// <summary>
    /// 직원 호감도 저장
    /// </summary>
    [ContextMenu("NPCFriendshipSave")]
    public void NPCFriendshipSave()
    {
        SaveLoadManager.Instance.m_NpcfirstTalkList = new List<bool>(npcFirstTalkList);
        //npcFriendShipList = new List<float>(AddNpcFriendShipList);
        SaveLoadManager.Instance.m_NpcFriendshipList = new List<float>(npcFriendShipList);
    }

    /// <summary>
    /// 직원 호감도 로드 및 하루 대화 초기화
    /// </summary>
    public void NPCFriendshipLoad()
    {
        npcFirstTalkList = new List<bool>(SaveLoadManager.Instance.m_NpcfirstTalkList);
        npcFriendShipList = new List<float>(SaveLoadManager.Instance.m_NpcFriendshipList);
        //AddNpcFriendShipList = new List<float>(SaveLoadManager.Instance.m_NpcFriendshipList);
        for (int i = 0; i < npcFriendShipList.Count; i++)
        {
            npcList[i].isFriendship = npcFriendShipList[i];
        }

        npcFirstTalkToDay = new List<bool>();
        for (int i = 0; i < npcList.Length; i++)
        {
            npcFirstTalkToDay.Add(false);
        }
    }

    /// <summary>
    /// 일일 대화 호감도 추가
    /// </summary>
    /// <param name="argCode">추가할 직원코드</param>
    public void AddFriendship(int argCode)
    {
        
        if(npcFirstTalkToDay[argCode] == false)
        {
            npcFirstTalkList[argCode] = true;
            npcFirstTalkToDay[argCode] = true;
            npcList[argCode].isFriendship += 1;
            npcFriendShipList[argCode] += 1;
            QuestManager.Instance.NpcQuestCheck(4, 1);
            QuestManager.Instance.NpcQuestCheck((argCode + 5), 1);
            //flag 호감도 상승(일일대화)
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 퀘스트 클리어 시 호감도 추가
    /// </summary>
    /// <param name="argCode">추가할 직원 코드</param>
    public void AddFriendshipQuestClear(int argCode)
    {
        npcList[argCode].isFriendship += 10;
        npcFriendShipList[argCode] += 10;
        //flag 직원 호감도 상승(퀘스트)
    }

    public static NPCManager Instance
    {
        get { return g_npcManager; }
    }
}
