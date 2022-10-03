using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DormitoryManager : MonoBehaviour
{
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
        filePath = Application.persistentDataPath + "/PlayerData";
        SaveLoadManager.Instance.Load(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);

        SceneController.Instance.FindObj();

        if (SaveLoadManager.Instance.m_IsFirst)
        {
            //ó�� ���� ����
            m_firstPanel.SetActive(true);
            m_DayLoadlPanel.SetActive(false);
            m_nextDayPanel.SetActive(false);
        }
        else if (GameValueManager.IsDay)
        {
            //���ӽ���
            m_firstPanel.SetActive(false);
            m_DayLoadlPanel.SetActive(true);
            m_nextDayPanel.SetActive(false);
        }
        else
        {
            //������
            m_firstPanel.SetActive(false);
            m_DayLoadlPanel.SetActive(false);
            m_nextDayPanel.SetActive(true);
        }

        m_Date.text = "DAY " + GameValueManager.Instance.IsDate.ToString("00");
        m_PlayerName.text = "�̸�: " + SaveLoadManager.Instance.m_PlayerName;
        m_Gold.text = "������: " + GameValueManager.Instance.IsGold + "G";
        m_Debt.text = "���� ä��: " + GameValueManager.Instance.IsDebt + "G";

        m_ReputationSlider.value = GameValueManager.Instance.IsReputation;
    }

    public void StartFirst()
    {
        SaveLoadManager.Instance.m_PlayerName = m_playerNameInput.text;
        SaveLoadManager.Instance.m_IsFirst = false;
        SaveLoadManager.Instance.Save(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);
        GameValueManager.IsDay = true;
        SceneController.Instance.LoadScene("MainGame");
    }

    public void PayBack()
    {
        GameValueManager.Instance.IsDebt -= GameValueManager.Instance.IsGold;
        QuestManager.Instance.NpcQuestCheck(1, GameValueManager.Instance.IsGold);

        GameValueManager.Instance.IsGold = 0;
        m_Gold.text = "������: " + GameValueManager.Instance.IsGold + "G";
        m_Debt.text = "���� ä��: " + GameValueManager.Instance.IsDebt + "G";
    }

    public void NextDay()
    {
        GameValueManager.Instance.ValueSave();
        SaveLoadManager.Instance.Save(filePath + SaveLoadManager.Instance.fileName[SaveLoadManager.Instance.fileSlotNum]);
        GameValueManager.IsDay = true;
        SceneController.Instance.LoadScene("MainGame");
    }

    public void GoWork()
    {
        GameValueManager.IsDay = true;
        SceneController.Instance.LoadScene("MainGame");
    }
}
