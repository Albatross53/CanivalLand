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
    /// ��ī�� ���¿���
    /// </summary>
    public List<bool> m_luckCardOpens = new List<bool>();
    /// <summary>
    /// ������ ��ī�� �ѹ�
    /// </summary>
    public int todayCardNum = 0;

    /// <summary>
    /// ������ ��ī�� �̹���
    /// </summary>
    public Sprite todayCardSprite;

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
        //todayCardNum = Random.Range(0, m_luckCardDatas.Length);
        todayCardNum = 0;
        todayCardSprite = m_luckCardDatas[todayCardNum].CardSprite;
        //m_luckCardOpens[todayCardNum] = true;
    }

    /// <summary>
    /// ��ī�� ���� ����
    /// </summary>
    public void LuckcardOpenSave()
    {
        SaveLoadManager.Instance.m_LuckCardOpen = new List<bool>(m_luckCardOpens);
    }

    /// <summary>
    /// ��ī�� ���� �ε�
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
