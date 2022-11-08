using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckcardManager : MonoBehaviour
{
    /// <summary>
    /// 운카드 데이터
    /// </summary>
    public LuckCardData[] m_luckCardDatas = null;
    /// <summary>
    /// 운카드 오픈여부
    /// </summary>
    public List<bool> m_luckCardOpens = new List<bool>();
    /// <summary>
    /// 오늘의 운카드 넘버
    /// </summary>
    public int todayCardNum = 0;

    /// <summary>
    /// 오늘의 운카드 이미지
    /// </summary>
    public Sprite todayCardSprite;

    /// <summary>
    /// 글로벌화
    /// </summary>
    static LuckcardManager g_luckcardManager;

    private void Awake()
    {
        //싱글톤
        if(Instance == null)
        {
            g_luckcardManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 운카드 뽑기
    /// </summary>
    public void RandomLuckacard()
    {
        //todayCardNum = Random.Range(0, m_luckCardDatas.Length);
        todayCardNum = 0;
        todayCardSprite = m_luckCardDatas[todayCardNum].CardSprite;
        //m_luckCardOpens[todayCardNum] = true;
    }

    /// <summary>
    /// 운카드 오픈 저장
    /// </summary>
    public void LuckcardOpenSave()
    {
        SaveLoadManager.Instance.m_LuckCardOpen = new List<bool>(m_luckCardOpens);
    }

    /// <summary>
    /// 운카드 오픈 로드
    /// </summary>
    public void LuckcardOpenLoad()
    {
        m_luckCardOpens = new List<bool>(SaveLoadManager.Instance.m_LuckCardOpen);
    }

    public static LuckcardManager Instance
    {
        get { return g_luckcardManager; }
    }
}
