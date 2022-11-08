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

    /// <summary>
    /// 놀이기구선호도 증가
    /// </summary>
    /// <param name="argCode">놀이기구코드</param>
    /// <param name="addNum">추가될 선호도값</param>
    public void AddPreference(int argCode, float addNum)
    {
        m_attractionPreferences[argCode] += addNum;
        if(m_attractionPreferences[argCode] >= maxPreference)
        {
            m_attractionPreferences[argCode] = maxPreference;
        }
    }

    public int bonus = 0;
    public void PreferenceEffect(int argCode)
    {
        bonus = Mathf.FloorToInt(m_attractionPreferences[argCode] / 10);
    }

    public static AttractionPreferenceManager Instance
    {
        get { return g_attractionPreference; }
    }
}
