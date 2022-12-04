using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int eventGenMaxNum = 5;

    /// <summary>
    /// 0. �̾�, 1. �нǹ�, 2. ������
    /// </summary>
    int eventNum;

    /// <summary>
    /// �̺�Ʈ ����
    /// </summary>
    [HideInInspector] public bool isEvent = false;

    /// <summary>
    /// �̺�Ʈ��ġ
    /// </summary>
    [SerializeField] Transform[] m_eventPoint;

    /// <summary>
    /// �̺�Ʈ ������Ʈ
    /// </summary>
    [SerializeField] GameObject[] m_eventObj;

    static EventManager g_eventManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_eventManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float genTime = Random.Range(0, 10);
        Invoke("EventGen", genTime);
    }

    public static EventManager Instance
    {
        get
        {
            return g_eventManager;
        }
    }

    public void EventGen()
    {
        if (!isEvent)
        {
            int eventGen = Random.Range(0, 10);
            if (eventGen < eventGenMaxNum)
            {
                isEvent = true;
                int ran = Random.Range(0, m_eventObj.Length);
                if (ran == 0)
                {
                    UIManager.Instance.FadeText("�̾ư� �߻��߽��ϴ�");
                }
                else if (ran == 1)
                {
                    UIManager.Instance.FadeText("�нǹ��� �߻��߽��ϴ�");
                }
                else if (ran == 2)
                {
                    UIManager.Instance.FadeText("�����Ⱑ �߻��߽��ϴ�");
                }
                EvenetStart(ran);
            }
        }
    }

    void EvenetStart(int argNum)
    {
        int PosNum = Random.Range(0, m_eventPoint.Length);
        m_eventObj[argNum].transform.position = m_eventPoint[PosNum].position;
    }
}
