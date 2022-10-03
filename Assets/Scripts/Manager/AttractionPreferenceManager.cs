using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionPreferenceManager : MonoBehaviour
{
    public List<float> m_attractionPreferences = new List<float>();
    float maxPreference = 100.0f;

    static AttractionPreferenceManager g_attractionPreference;

    private void Awake()
    {
        if(Instance == null)
        {
            g_attractionPreference = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PreferenceSave()
    {
        SaveLoadManager.Instance.m_AttractionPreferenceList = new List<float>(m_attractionPreferences);
    }
    public void PreferenceLoad()
    {
        m_attractionPreferences = new List<float>(SaveLoadManager.Instance.m_AttractionPreferenceList);
    }

    public void AddPreference(int argNum)
    {
        maxPreference = ParkReputationManager.Instance.parkReputation;
        m_attractionPreferences[argNum] += 10;
        if(m_attractionPreferences[argNum] >= maxPreference)
        {
            m_attractionPreferences[argNum] = maxPreference;
        }
    }

    public static AttractionPreferenceManager Instance
    {
        get { return g_attractionPreference; }
    }
}
