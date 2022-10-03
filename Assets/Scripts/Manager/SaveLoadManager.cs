using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    public bool m_IsFirst = true;
    public string m_PlayerName = "empty";
    public float m_Reputation = 0;
    public int m_Date = 0;
    public int m_Gold = 0;
    public int m_Debt = 100000000;
    public int m_NpcQuestNum = 0;
    public float m_ParkReputation = 0;
    public List<bool> m_LuckCardOpen = new List<bool>();
    public List<bool> m_EndingOpen = new List<bool>();
    public List<float> m_AttractionPreferenceList = new List<float>();
    public List<float> m_NpcFriendshipList = new List<float>();
    public List<bool> m_NpcfirstTalkList = new List<bool>();

    public List<bool> m_NpcQuestIsStart = new List<bool>();
    public List<bool> m_NpcQuestIsClear = new List<bool>();
    public int m_NpcQuestCurNum = 0;

    PlayerData _playerData;

    string filePath = null;

    public List<string> fileName;

    public int fileSlotNum;

    static SaveLoadManager g_saveLoadManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_saveLoadManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
    private void Start()
    {
        filePath = Application.persistentDataPath + "/PlayerData";

        for(int i = 0; i < fileName.Count; i++)
        {
            Load(filePath + fileName[i]);
        }
    }
    */

    public void LoadStart(int argNum)
    {
        fileSlotNum = argNum;
        Load(filePath + fileName[fileSlotNum]);
    }

    /// <summary>
    /// 데이터로드
    /// </summary>
    public void Load(string argPath)
    {
        if (!File.Exists(argPath))
        {
            //reset
            Save(argPath);
            return;
        }
        string jdata = File.ReadAllText(argPath);
        _playerData = JsonUtility.FromJson<PlayerData>(jdata);

        m_IsFirst = _playerData.isFirst;
        m_PlayerName = _playerData.playerName;
        m_Reputation = _playerData.reputation;
        m_Date = _playerData.date;
        m_Gold = _playerData.gold;
        m_Debt = _playerData.debt;
        m_NpcQuestNum = _playerData.npcQuestNum;
        m_ParkReputation = _playerData.parkReputation;
        
        m_LuckCardOpen = new List<bool>(_playerData.LuckCardOpen);
        m_EndingOpen = new List<bool>(_playerData.EndingOpen);
        m_AttractionPreferenceList = new List<float>(_playerData.AttractionPreferenceList);
        m_NpcFriendshipList = new List<float>(_playerData.NpcFriendshipList);
        m_NpcfirstTalkList = new List<bool>(_playerData.NpcfirstTalkList);

        m_NpcQuestIsStart = new List<bool>(_playerData.NpcQuestIsStart);
        m_NpcQuestIsClear = new List<bool>(_playerData.NpcQuestIsClear);
        m_NpcQuestCurNum = _playerData.npcQuestCurNum;
    }

    /// <summary>
    /// 데이터세이브
    /// </summary>
    public void Save(string argPath)
    {
        _playerData = new PlayerData(m_IsFirst, m_PlayerName, m_Reputation, 
            m_Date, m_Gold, m_Debt, m_NpcQuestNum, m_ParkReputation,
            m_LuckCardOpen, m_EndingOpen, m_AttractionPreferenceList,
            m_NpcFriendshipList, m_NpcfirstTalkList, m_NpcQuestIsStart, m_NpcQuestIsClear, m_NpcQuestCurNum);
        string jdata = JsonUtility.ToJson(_playerData);
        File.WriteAllText(argPath, jdata);
        Load(argPath);
    }

    public static SaveLoadManager Instance
    {
        get { return g_saveLoadManager; }
    }
}

[Serializable]
public class PlayerData
{
    public bool isFirst;
    public string playerName;
    public float reputation;
    public int date;
    public int gold;
    public int debt;
    public int npcQuestNum;
    public float parkReputation;
    public List<bool> LuckCardOpen = new List<bool>();
    public List<bool> EndingOpen = new List<bool>();
    public List<float> AttractionPreferenceList = new List<float>();
    public List<float> NpcFriendshipList = new List<float>();
    public List<bool> NpcfirstTalkList = new List<bool>();

    public List<bool> NpcQuestIsStart = new List<bool>();
    public List<bool> NpcQuestIsClear = new List<bool>();
    public int npcQuestCurNum;

    public PlayerData(bool isFirst, string playerName, float reputation, 
        int date, int gold, int debt, int npcQuestNum, float parkReputation, 
        List<bool> luckCardOpen, List<bool> endingOpen, List<float> attractionPreferenceList, 
        List<float> npcFriendshipList, List<bool> npcfirstTalkList, List<bool> npcQuestIsStart, 
        List<bool> npcQuestIsClear, int npcQuestCurNum)
    {
        this.isFirst = isFirst;
        this.playerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
        this.reputation = reputation;
        this.date = date;
        this.gold = gold;
        this.debt = debt;
        this.npcQuestNum = npcQuestNum;
        this.parkReputation = parkReputation;
        LuckCardOpen = luckCardOpen ?? throw new ArgumentNullException(nameof(luckCardOpen));
        EndingOpen = endingOpen ?? throw new ArgumentNullException(nameof(endingOpen));
        AttractionPreferenceList = attractionPreferenceList ?? throw new ArgumentNullException(nameof(attractionPreferenceList));
        NpcFriendshipList = npcFriendshipList ?? throw new ArgumentNullException(nameof(npcFriendshipList));
        NpcfirstTalkList = npcfirstTalkList ?? throw new ArgumentNullException(nameof(npcfirstTalkList));
        NpcQuestIsStart = npcQuestIsStart ?? throw new ArgumentNullException(nameof(npcQuestIsStart));
        NpcQuestIsClear = npcQuestIsClear ?? throw new ArgumentNullException(nameof(npcQuestIsClear));
        this.npcQuestCurNum = npcQuestCurNum;
    }
}

