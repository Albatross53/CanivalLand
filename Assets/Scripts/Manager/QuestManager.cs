using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ����Ʈ �й� �� ���� Ȯ��
/// </summary>
public class QuestManager : MonoBehaviour
{
    [SerializeField] AudioClip npcQuestClear;
    [SerializeField] AudioClip dayQuestClear;

    /// <summary>
    /// ����Ʈ ������
    /// </summary>
    public QuestData[] m_questData;

    /// <summary>
    /// npc����Ʈ ������
    /// </summary>
    public NPCQuestData[] m_npcQuestData;

    /// <summary>
    /// ������ ����Ʈ����Ʈ
    /// </summary>
    public List<int> m_todayQuestNum;

    /// <summary>
    /// ����Ʈ Ȯ�θ���Ʈ
    /// </summary>
    public List<bool> m_DayQuestisClear;

    /// <summary>
    /// ���� ����Ʈ ����
    /// </summary>
    public List<bool> m_NpcQuestIsStart;
    /// <summary>
    /// ��������Ʈ Ŭ����
    /// </summary>
    public List<bool> m_NpcQuestIsClear;

    /// <summary>
    /// ���� npc����Ʈ �ѹ�
    /// </summary>
    public int NpcQuestNum = 0;

    int npcQuestCurNum = 0;
    /// <summary>
    /// ����Ʈ�� ���̴� ����
    /// </summary>
    public int NpcQuestCurNum
    {
        get { return npcQuestCurNum; }
        set
        {
            npcQuestCurNum = value;
            if (npcQuestCurNum >= m_npcQuestData[NpcQuestNum].QuestMaxNum)
            {
                npcQuestCurNum = m_npcQuestData[NpcQuestNum].QuestMaxNum;
                m_NpcQuestIsClear[NpcQuestNum] = true;
            }
        }
    }

    /// <summary>
    /// ����Ʈ�Ŵ����۷ι�ȭ
    /// </summary>
    static QuestManager g_questManager;
    private void Awake()
    {
        if (Instance == null)
        {
            g_questManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ����Ʈ �й�
    /// </summary>
    public void StartGame()
    {
        m_todayQuestNum = new List<int>();
        m_DayQuestisClear = new List<bool>();

        for(int i = 0; i< m_questData.Length; i++)
        {
            m_DayQuestisClear.Add(true);
        }

        for(int i = 0; i < 5; i++)
        {
            int QuestNum = Random.Range(1, m_questData.Length);
            m_todayQuestNum.Add(QuestNum);
        }
        m_todayQuestNum = m_todayQuestNum.Distinct().ToList();

        for(int i = 0; i < m_todayQuestNum.Count; i++)
        {
            m_DayQuestisClear[m_todayQuestNum[i]] = false;
        }
    }


    public void QuestSave()
    {
        SaveLoadManager.Instance.m_NpcQuestNum = NpcQuestNum;
        SaveLoadManager.Instance.m_NpcQuestIsStart = new List<bool>(m_NpcQuestIsStart);
        SaveLoadManager.Instance.m_NpcQuestIsClear = new List<bool>(m_NpcQuestIsClear);
        SaveLoadManager.Instance.m_NpcQuestCurNum = NpcQuestCurNum;
    }
    public void QuestLoad()
    {
        NpcQuestNum = SaveLoadManager.Instance.m_NpcQuestNum;
        m_NpcQuestIsStart = new List<bool>(SaveLoadManager.Instance.m_NpcQuestIsStart);
        m_NpcQuestIsClear = new List<bool>(SaveLoadManager.Instance.m_NpcQuestIsClear);
        NpcQuestCurNum = SaveLoadManager.Instance.m_NpcQuestCurNum;
    }

    /// <summary>
    /// ��������Ʈ ���� Ȯ�� �Լ�
    /// </summary>
    /// <param name="argType">����Ʈ Ÿ��</param>
    /// <param name="argNum">������ ��</param>
    public void NpcQuestCheck(int argType, int argNum)
    {
        SoundManager.Instance.PlayEffectSound(npcQuestClear);

        if (m_npcQuestData[NpcQuestNum].QuestIsType == argType)
        {
            NpcQuestCurNum += argNum;
        }
        return;
    }

    /// <summary>
    /// ���� ����Ʈ ���� Ȯ�� �Լ�
    /// </summary>
    /// <param name="argNum">����Ʈ ���ప</param>
    public void QuestCheck(int argNum)
    {
        SoundManager.Instance.PlayEffectSound(dayQuestClear);

        if (m_todayQuestNum.Contains(argNum))
        {
            int questIndex = m_todayQuestNum.IndexOf(argNum);
            if(m_DayQuestisClear[m_todayQuestNum[questIndex]] == false)
            {
                m_DayQuestisClear[m_todayQuestNum[questIndex]] = true;
                GameValueManager.Instance.IsGold += 
                    (m_questData[questIndex].QuestReward + 50 + (AttractionPreferenceManager.Instance.bonus * 10))
                    * LuckcardManager.Instance.m_luckCardDatas[LuckcardManager.Instance.todayCardNum].ScoreAffecting;
            }
            else
            {
                GameValueManager.Instance.IsGold += 
                    (50 + (AttractionPreferenceManager.Instance.bonus * 10)) * LuckcardManager.Instance.m_luckCardDatas[LuckcardManager.Instance.todayCardNum].ScoreAffecting;
            }
        }
        else if(argNum != 0)
        {
            GameValueManager.Instance.IsGold += 
                (50 * LuckcardManager.Instance.m_luckCardDatas[LuckcardManager.Instance.todayCardNum].ScoreAffecting);
            //����Ʈ�� �ƴ� �̴ϰ���Ŭ����
        }
        else
        {
            return;
        }
    }

    public static QuestManager Instance
    {
        get { return g_questManager; }
    }
}
