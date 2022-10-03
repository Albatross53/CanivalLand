using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Commons")]
    /// <summary>
    /// ��, �� �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text m_TimeText;
    /// <summary>
    /// ���ӳ��� �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text m_DateText = null;
    /// <summary>
    /// ���̰��� ����
    /// </summary>
    [SerializeField] Image m_ParkReputation;
    /// <summary>
    /// �ð� �̹���
    /// </summary>
    [SerializeField] Image timeImage;
    /// <summary>
    /// �ð��� ��������Ʈ
    /// </summary>
    [SerializeField] Sprite[] timeSprite;

    [Header("Option")]
    /// <summary>
    /// �ɼ�â
    /// </summary>
    [SerializeField] GameObject OptionUI;
    /// <summary>
    /// �ɼ��г� �迭
    /// </summary>
    [SerializeField] GameObject[] m_OptionPanel;
    /// <summary>
    /// ����ɼ��гγѹ�
    /// </summary>
    int OptionNum = 0;

    [Header("NomalQuest")]
    /// <summary>
    /// ����Ʈ �ؽ�Ʈ 5��
    /// </summary>
    [SerializeField] GameObject[] m_questObj;
    [SerializeField] Text[] m_questName;
    [SerializeField] Text[] m_questDis;
    [SerializeField] Text[] m_questReward;
    [SerializeField] Toggle[] m_questClear;

    [Header("selfDis")]
    /// <summary>
    /// �÷��̾� ���� ���, ��, ��¥�ؽ�Ʈ
    /// </summary>
    [SerializeField] Text m_PlayerName;
    [SerializeField] Text m_OptionGoldText;
    [SerializeField] Text m_OptionDebtText;
    [SerializeField] Text m_OptionDayText;
    [SerializeField] Slider m_ParkSlider_0;
    [SerializeField] Text m_ParkRateText;
    [SerializeField] Slider m_ParkSlider_1;


    [Header("TodayLuckCard")]
    /// <summary>
    /// ������ ��ī�� �̹���
    /// </summary>
    [SerializeField] Image m_OptionCardImage;
    /// <summary>
    /// ������ ��ī���ڵ�, ��ī���̸�, ��ī�� ���� �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text m_LuckCardName, m_LuckCradDis;


    [Header("NpcQuest")]
    /// <summary>
    /// npc����Ʈ �ؽ�Ʈ(������, �̸�, ����, ������� ����)
    /// </summary>
    [SerializeField] Text m_NpcQuestOrderText;
    [SerializeField] Text m_NpcQuestNameText;
    [SerializeField] Text m_NpcQuestDisText;
    [SerializeField] Text m_NpcQuestIsText;

    [Header("CardCacha")]
    [SerializeField] GameObject m_LuckcardCacha;
    [SerializeField] Image m_CachaCardImage;
    [SerializeField] Button m_LuckcardCachaBtn, m_LuckcardEnterBtn;

    [Header("Empty")]
    [SerializeField] Text viking;
    [SerializeField] Text restaurant;

    [Header("Event")]
    /// <summary>
    /// �̺�Ʈ ������ �̹���
    /// </summary>
    [SerializeField] Sprite trash, lostObj, lostKid;
    /// <summary>
    /// �ݱ�� �Ӹ� ������Ʈ
    /// </summary>
    [SerializeField] Image g_PickUp;


    static UIManager g_UIManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_UIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameValueManager.IsDay)
        {
            m_LuckcardCacha.SetActive(true);
            GameManager.Instance.m_optionOpen = true;
            m_LuckcardEnterBtn.interactable = false;
        }
        else
        {
            m_LuckcardCacha.SetActive(false);
            OptionUI.SetActive(false);
        }

        OptionNum = 0;
    }

    private void Update()
    {
        m_ParkReputation.fillAmount = ParkReputationManager.Instance.parkReputation / 1000;
        m_DateText.text = "Day " + GameValueManager.Instance.IsDate.ToString("00");
        m_TimeText.text = TimeManager.Instance.IsMin.ToString("00") + " : " + TimeManager.Instance.IsSec.ToString("00");

        if (OptionUI.activeSelf == true)
        {
            GameManager.Instance.m_optionOpen = true;
            DayQuestView();
        }
        else if (OptionUI.activeSelf == false)
        {
            GameManager.Instance.m_optionOpen = false;
        }


        if (m_LuckcardCacha.activeSelf == true)
        {
            GameManager.Instance.m_optionOpen = true;

        } else if (m_LuckcardCacha.activeSelf == false)
        {
            GameManager.Instance.m_optionOpen = false;
        }

    }

    /// <summary>
    /// �������ݱ�
    /// </summary>
    /// <param name="argItemCode">������ �ڵ�</param>
    public void PickUp(int argItemCode)
    {
        PlayerController.Instance.m_PickCode = argItemCode;
        g_PickUp.gameObject.SetActive(true);
        PickUpSprite(argItemCode);
    }

    /// <summary>
    /// ������ ��������
    /// </summary>
    public void PickDown()
    {
        g_PickUp.gameObject.SetActive(false);
        QuestManager.Instance.QuestCheck(PlayerController.Instance.m_PickCode + 6);
        QuestManager.Instance.NpcQuestCheck(2, 1);
        GameValueManager.Instance.IsGold += 10;
    }

    /// <summary>
    /// ������ �ֿ��� �κ��丮 ���
    /// </summary>
    /// <param name="argItemCode">�������ڵ�</param>
    void PickUpSprite(int argItemCode)
    {
        //lostkid 0
        //lostobj 1
        //transh 2
        switch (argItemCode)
        {
            case 0:
                g_PickUp.GetComponent<Image>().sprite = lostKid;
                break;
            case 1:
                g_PickUp.GetComponent<Image>().sprite = lostObj;
                break;
            case 2:
                g_PickUp.GetComponent<Image>().sprite = trash;
                break;
        }
    }

    /// <summary>
    /// �ð� �̹��� �ٲٱ�
    /// </summary>
    /// <param name="argNum">�ٲ� ����</param>
    public void SetTimeSprite(int argNum)
    {
        timeImage.sprite = timeSprite[argNum];
    }

    /// <summary>
    /// ī�� ��í
    /// </summary>
    public void LuckcardCacha()
    {
        LuckcardManager.Instance.RandomLuckacard();
        m_LuckcardCachaBtn.interactable = false;
        CardImage();
    }

    /// <summary>
    /// ī�� �̹��� �ٲٱ�
    /// </summary>
    public void CardImage()
    {
        m_CachaCardImage.gameObject.SetActive(true);

        m_CachaCardImage.sprite =
            LuckcardManager.Instance.todayCardSprite;

        m_LuckcardEnterBtn.interactable = true;
    }

    /// <summary>
    /// �ɼ��г� �ٲٱ�
    /// </summary>
    /// <param name="argNum">�ٲٴ� �ɼ��гγѹ�</param>
    public void SetOption(int argNum)
    {
        if(OptionNum != 0)
        {
            m_OptionPanel[OptionNum].SetActive(false);
        }

        m_OptionPanel[argNum].SetActive(true);
        OptionNum = argNum;

        if (argNum == 1)//�ڱ�Ұ�
        {
            m_PlayerName.text = SaveLoadManager.Instance.m_PlayerName;
            m_OptionGoldText.text = GameValueManager.Instance.IsGold + " G";
            m_OptionDayText.text = (21 - GameValueManager.Instance.IsDate).ToString() + "�� ����";
            m_OptionDebtText.text = "����ä�� : " + GameValueManager.Instance.IsDebt;

            m_ParkRateText.text = "B.B���� ����: " + 
                Mathf.RoundToInt(ParkReputationManager.Instance.parkReputation / 100).ToString("00") + " ���";
            m_ParkSlider_0.value = ParkReputationManager.Instance.parkReputation;
        }
        else if (argNum == 2) //��ī��
        {
            int num = LuckcardManager.Instance.todayCardNum;
            m_OptionCardImage.sprite = LuckcardManager.Instance.m_luckCardDatas[num].CardSprite;
            m_LuckCardName.text = LuckcardManager.Instance.m_luckCardDatas[num].CardName;
            m_LuckCradDis.text = LuckcardManager.Instance.m_luckCardDatas[num].CardDisc;
        }
        else if (argNum == 3)//npc����Ʈ
        {
            NpcQuestView();
        }
        else if (argNum == 4) //ģ�е�
        {
            NPCManager.Instance.Option();
        }
        else if (argNum == 5) // ���̰��� �
        {
            m_ParkSlider_1.value = ParkReputationManager.Instance.parkReputation;

            //�ӽ�
            if(QuestManager.Instance.NpcQuestNum >= 2)
            {
                viking.text = "����ŷ ����";
                //restaurant.text = "�Ĵ� ����";
            }
        }
        else if (argNum == 6) // ���Ӽ���
        {

        }
    }

    /// <summary>
    /// ����Ʈ �ɼ�â ǥ��
    /// </summary>
    void DayQuestView()
    {
        QuestManager QM = QuestManager.Instance;
        
        for (int i = 0; i < QM.m_todayQuestNum.Count; i++)
        {
            m_questObj[i].SetActive(true);
            m_questName[i].text = QM.m_questData[QM.m_todayQuestNum[i]].QuestName;
            m_questDis[i].text = QM.m_questData[QM.m_todayQuestNum[i]].QuestDisc;
            m_questReward[i].text = QM.m_questData[QM.m_todayQuestNum[i]].QuestReward.ToString() + " G";
            m_questClear[i].isOn = QM.m_DayQuestisClear[QM.m_todayQuestNum[i]];

        }
    }

    /// <summary>
    /// npc����Ʈ ����
    /// </summary>
    void NpcQuestView()
    {
        QuestManager QM = QuestManager.Instance;
        
        if (QM.m_NpcQuestIsStart[QM.NpcQuestNum])
        {
            m_NpcQuestOrderText.text = QM.m_npcQuestData[QM.NpcQuestNum].NPCQuestOrder;
            m_NpcQuestNameText.text = QM.m_npcQuestData[QM.NpcQuestNum].NPCQuestName;
            m_NpcQuestDisText.text = QM.m_npcQuestData[QM.NpcQuestNum].NPCQuestDisc;
            m_NpcQuestIsText.text = QM.NpcQuestCurNum + " / " + QM.m_npcQuestData[QM.NpcQuestNum].QuestMaxNum.ToString();
        }
        else
        {
            m_NpcQuestOrderText.text = "null";
            m_NpcQuestNameText.text = "null";
            m_NpcQuestDisText.text = "null";
            m_NpcQuestIsText.text = "null";
        }
    }

    /// <summary>
    /// ������ ��ư
    /// </summary>
    public void ExitBtn()
    {
        SceneController.Instance.GameExit();
    }

    public static UIManager Instance
    {
        get { return g_UIManager; }
    }
}
