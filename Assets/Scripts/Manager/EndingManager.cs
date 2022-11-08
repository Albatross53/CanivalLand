using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public EndingData[] m_endingDatas = null;

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
    
    public static EndingManager Instance
    {
        get { return g_endingManager; }
    }
}
