using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public EndingData[] m_endingDatas = null;
    public List<bool> m_endingOpens = new List<bool>();

    static EndingManager g_endingManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_endingManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void EndingOpenSave()
    {
        SaveLoadManager.Instance.m_EndingOpen = new List<bool>(m_endingOpens);
    }

    public void EndingOpenLoad()
    {
        m_endingOpens = new List<bool>(SaveLoadManager.Instance.m_EndingOpen);
    }

    public static EndingManager Instance
    {
        get { return g_endingManager; }
    }
}
