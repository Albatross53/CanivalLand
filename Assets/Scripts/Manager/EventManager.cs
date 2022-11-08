using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int eventGenMaxNum = 2;

    /// <summary>
    /// 0. 미아, 1. 분실물, 2. 쓰레기
    /// </summary>
    int eventNum;

    /// <summary>
    /// 이벤트 여부
    /// </summary>
    [HideInInspector] public bool isEvent = false;

    /// <summary>
    /// 이벤트위치
    /// </summary>
    [SerializeField] Transform[] m_eventPoint;

    /// <summary>
    /// 이벤트 오브젝트
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
        if(eventGen < eventGenMaxNum)
        {
            isEvent = true;
            int ran = Random.Range(0, m_eventObj.Length);
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
