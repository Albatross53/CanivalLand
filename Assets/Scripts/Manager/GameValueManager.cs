using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValueManager : MonoBehaviour
{
    public static bool IsDay = true;

    int workingNum = 0;
    public int IsWorkimg
    {
        get { return workingNum; }
        set { workingNum = value;
        }
    }

    float reputation = 0;
    public float IsReputation
    {
        get { return reputation; }
        set { reputation = value; }
    }

    int m_debt = 10000000;
    public int IsDebt
    {
        get { return m_debt; }
        set { m_debt = value; }
    }

    /// <summary>
    /// 게임돈
    /// </summary>
    int m_gold = 0;
    public int IsGold
    {
        get { return m_gold; }
        set
        {
            m_gold = value;
            if (m_gold < 0)
            {
                m_gold = 0;
                return;
            }
        }
    }

    /// <summary>
    /// 게임날짜
    /// </summary>
    int m_date = 1;
    public int IsDate
    {
        get { return m_date; }
        set
        {
            m_date = value;
        }
    }

    int m_attractionPosCode;
    public int IsAttractionPosCode
    {
        get { return m_attractionPosCode; }
        set
        {
            m_attractionPosCode = value;
        }
    }

    int miniGameScore;
    public int IsMiniGameScore
    {
        get { return miniGameScore; }
        set
        {
            miniGameScore = value;
        }
    }

    /// <summary>
    /// 셀프 글로벌화
    /// </summary>
    static GameValueManager g_gameValueManager = null;

    private void Awake()
    {
        if (Instance == null)
        {
            g_gameValueManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ValueSave()
    {
        SaveLoadManager svn = SaveLoadManager.Instance;
        svn.m_Date = IsDate;
        svn.m_Gold = IsGold;
        svn.m_Debt = IsDebt;
        svn.m_Reputation = IsReputation;
    }

    public void ValueLoad()
    {
        SaveLoadManager svn = SaveLoadManager.Instance;
        IsDate = svn.m_Date;
        IsGold = svn.m_Gold;
        IsDebt = svn.m_Debt;
        IsReputation = svn.m_Reputation;
    }

    /// <summary>
    /// 미니게임이 끝났을때 추가하는 점수와 시간
    /// </summary>
    /// <param name="argGold">추가할 점수</param>
    /// <param name="argTime">추가할 시간(단위 분)</param>
    public void MiniGameFinsh(int argGold, int argTime)
    {
        IsGold += argGold;
        TimeManager.Instance.IsMin += argTime;
    }

    public static GameValueManager Instance
    {
        get { return g_gameValueManager; }
    }
}
