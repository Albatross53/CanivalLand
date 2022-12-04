using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public EvidenceData[] m_EvidenceDatas = null;
    public int CurEvidenceNum = 0;
    static EvidenceManager g_EvidenceManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_EvidenceManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EvidenceSave()
    {
        SaveLoadManager.Instance.m_CurEvidenceNum = CurEvidenceNum;
    }

    public void EvidenceLoad()
    {
        CurEvidenceNum = SaveLoadManager.Instance.m_CurEvidenceNum;
    }


    public static EvidenceManager Instance
    {
        get { return g_EvidenceManager; }
    }
}
