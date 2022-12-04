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
    /// 오늘의 운카드 넘버
    /// </summary>
    public int todayCardNum = 0;

    /// <summary>
    /// 오늘의 운카드 이미지
    /// </summary>
    public Sprite todayCardSprite;

    public int todayAffectingNum = 0;

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
        todayCardNum = Random.Range(0, 2);
        todayCardSprite = m_luckCardDatas[todayCardNum].CardSprite;
        todayAffectingNum = m_luckCardDatas[todayCardNum].ScoreAffecting;
    }

    public static LuckcardManager Instance
    {
        get { return g_luckcardManager; }
    }
}
