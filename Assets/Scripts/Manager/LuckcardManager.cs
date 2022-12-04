using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckcardManager : MonoBehaviour
{
    /// <summary>
    /// ��ī�� ������
    /// </summary>
    public LuckCardData[] m_luckCardDatas = null;

    /// <summary>
    /// ������ ��ī�� �ѹ�
    /// </summary>
    public int todayCardNum = 0;

    /// <summary>
    /// ������ ��ī�� �̹���
    /// </summary>
    public Sprite todayCardSprite;

    public int todayAffectingNum = 0;

    /// <summary>
    /// �۷ι�ȭ
    /// </summary>
    static LuckcardManager g_luckcardManager;

    private void Awake()
    {
        //�̱���
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
    /// ��ī�� �̱�
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
