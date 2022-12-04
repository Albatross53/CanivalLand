 using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class LuckCardController : MonoBehaviour
{
    int eventGenMaxNum = 5;
    public int curEventNum = 0;
    float waitTime = 1f;

    [SerializeField] GameObject zombi;

    /// <summary>
    /// �̺�Ʈ��ġ
    /// </summary>
    [SerializeField] Transform[] m_eventPoint;

    static LuckCardController g_LuckCardController;

    private void Awake()
    {
        if(Instance == null)
        {
            g_LuckCardController = this;
        }
    }

    private void Start()
    {
        InvokeRepeating("EventGen", 3, waitTime);
    }

    void EventGen()
    {
        if (curEventNum < eventGenMaxNum)
        {
            if (LuckcardManager.Instance.todayCardNum == 1)
            {
                int ranNum = Random.Range(0, m_eventPoint.Length);
                GameObject luckObj = Instantiate(zombi, m_eventPoint[ranNum]);
                curEventNum++;
                UIManager.Instance.FadeText("���� ��Ÿ�����ϴ�");
                waitTime = Random.Range(0, 13);
            }
        }
    }

    public static LuckCardController Instance
    {
        get { return g_LuckCardController; }
    }
}
