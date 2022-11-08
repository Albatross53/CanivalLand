using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkReputationManager : MonoBehaviour
{
    public float parkReputation = 0;
    static ParkReputationManager g_reputationManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_reputationManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReputationSave()
    {
        parkReputation = SaveLoadManager.Instance.m_ParkReputation;
    }

    public void ReputationLoad()
    {
        SaveLoadManager.Instance.m_ParkReputation = parkReputation;
    }

    public void addReputation(int argNum)
    {
        parkReputation += argNum;
        if(parkReputation >= 1000)
        {
            parkReputation = 1000;
        }
    }

    public static ParkReputationManager Instance
    {
        get { return g_reputationManager; }
    }
}
