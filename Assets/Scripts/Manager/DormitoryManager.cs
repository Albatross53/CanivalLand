using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DormitoryManager : MonoBehaviour
{
    [SerializeField] AudioClip dormBack;

    [Header("effect")]
    [SerializeField] AudioClip startClip;
    [SerializeField] AudioClip nextClip;
    [SerializeField] AudioClip paybackClip;

    [Header("common")]
    [SerializeField] Text m_Date;
    [SerializeField] Text m_PlayerName;
    [SerializeField] Text m_Debt;
    [SerializeField] Text m_Gold;
    [SerializeField] Slider m_ReputationSlider;

    [Header("Panel")]
    [SerializeField] GameObject m_firstPanel;
    [SerializeField] GameObject m_nextDayPanel;
    [SerializeField] GameObject m_DayLoadlPanel;

    [Header("other")]
    [SerializeField] InputField m_playerNameInput;
    string filePath;

    private void Start()
    {
        SoundManager.Instance.PlayBackSound(dormBack);

        filePath = Application.persistentDataPath + "/PlayerData";
        SaveLoadManager.Instance.Load(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);

        SceneController.Instance.FindObj();

        if (SaveLoadManager.Instance.m_IsFirst)
        {
            //처음 게임 시작
            m_firstPanel.SetActive(true);
            m_DayLoadlPanel.SetActive(false);
            m_nextDayPanel.SetActive(false);
        }
        else if (GameValueManager.IsDay)
        {
            //게임시작
            m_firstPanel.SetActive(false);
            m_DayLoadlPanel.SetActive(true);
            m_nextDayPanel.SetActive(false);
        }
        else
        {
            //게임중
            m_firstPanel.SetActive(false);
            m_DayLoadlPanel.SetActive(false);
            m_nextDayPanel.SetActive(true);
        }

        m_Date.text = "DAY " + SaveLoadManager.Instance.m_Date.ToString("00");
        m_PlayerName.text = "이름: " + SaveLoadManager.Instance.m_PlayerName;
        m_Gold.text = "소지금: " + SaveLoadManager.Instance.m_Gold + "G";
        m_Debt.text = "현재 채무: " + SaveLoadManager.Instance.m_Debt + "G";

        m_ReputationSlider.value = GameValueManager.Instance.IsReputation;
    }

    public void StartFirst()
    {
        SoundManager.Instance.PlayEffectSound(startClip);
        SaveLoadManager.Instance.m_PlayerName = m_playerNameInput.text;
        SaveLoadManager.Instance.m_IsFirst = false;
        SaveLoadManager.Instance.Save(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);
        GameValueManager.IsDay = true;
        SceneController.Instance.LoadScene("MainGame");
    }

    public void PayBack()
    {
        SoundManager.Instance.PlayEffectSound(paybackClip);

        GameValueManager.Instance.IsDebt -= GameValueManager.Instance.IsGold;
        QuestManager.Instance.NpcQuestCheck(1, GameValueManager.Instance.IsGold);

        GameValueManager.Instance.IsGold = 0;
        m_Gold.text = "소지금: " + GameValueManager.Instance.IsGold + "G";
        m_Debt.text = "현재 채무: " + GameValueManager.Instance.IsDebt + "G";
    }

    public void NextDay()
    {
        SoundManager.Instance.PlayEffectSound(nextClip);

        GameValueManager.Instance.ValueSave();
        SaveLoadManager.Instance.Save(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);
        GameValueManager.IsDay = true;

        if (SaveLoadManager.Instance.m_Date > 20)
        {
            SceneController.Instance.LoadScene("Ending");
        }
        else
        {
            SceneController.Instance.LoadScene("MainGame");
        }
    }

    public void GoWork()
    {
        SoundManager.Instance.PlayEffectSound(startClip);

        GameValueManager.IsDay = true;

        if (SaveLoadManager.Instance.m_Date > 20)
        {
            SceneController.Instance.LoadScene("Ending");
        }
        else
        {
            SceneController.Instance.LoadScene("MainGame");
        }
    }
}
