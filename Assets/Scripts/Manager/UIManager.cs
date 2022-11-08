using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioClip catchaClip;
    [SerializeField] AudioClip pickUpCilp;

    [Header("Commons")]
    /// <summary>
    /// 분, 초 텍스트
    /// </summary>
    [SerializeField] Text m_TimeText;
    /// <summary>
    /// 게임날자 텍스트
    /// </summary>
    [SerializeField] Text m_DateText = null;
    /// <summary>
    /// 놀이공원 평판
    /// </summary>
    [SerializeField] Image m_ParkReputation;
    /// <summary>
    /// 시간 이미지
    /// </summary>
    [SerializeField] Image timeImage;
    /// <summary>
    /// 시간별 스프라이트
    /// </summary>
    [SerializeField] Sprite[] timeSprite;

    [Header("Option")]
    /// <summary>
    /// 옵션창
    /// </summary>
    [SerializeField] GameObject OptionUI;
    /// <summary>
    /// 옵션패널 배열
    /// </summary>
    [SerializeField] GameObject[] m_OptionPanel;
    /// <summary>
    /// 현재옵션패널넘버
    /// </summary>
    int OptionNum = 0;

    [Header("NomalQuest")]
    /// <summary>
    /// 퀘스트 텍스트 5개
    /// </summary>
    [SerializeField] GameObject[] m_questObj;
    [SerializeField] Text[] m_questName;
    [SerializeField] Text[] m_questDis;
    [SerializeField] Text[] m_questReward;
    [SerializeField] Toggle[] m_questClear;

    [Header("selfDis")]
    /// <summary>
    /// 플레이어 정보 골드, 빛, 날짜텍스트
    /// </summary>
    [SerializeField] Text m_PlayerName;
    [SerializeField] Text m_OptionGoldText;
    [SerializeField] Text m_OptionDebtText;
    [SerializeField] Text m_OptionDayText;
    [SerializeField] Slider m_ParkSlider_0;
    [SerializeField] Text m_ParkRateText;

    [Header("TodayLuckCard")]
    /// <summary>
    /// 오늘의 운카드 이미지
    /// </summary>
    [SerializeField] Image m_OptionCardImage;
    /// <summary>
    /// 오늘의 운카드코드, 운카드이름, 운카드 설명 텍스트
    /// </summary>
    [SerializeField] Text m_LuckCardName, m_LuckCradDis;


    [Header("NpcQuest")]
    /// <summary>
    /// npc퀘스트 텍스트(수주자, 이름, 설명, 현재상태 숫자)
    /// </summary>
    [SerializeField] Text m_NpcQuestOrderText;
    [SerializeField] Text m_NpcQuestNameText;
    [SerializeField] Text m_NpcQuestDisText;
    [SerializeField] Text m_NpcQuestIsText;

    [Header("CardCacha")]
    [SerializeField] GameObject m_LuckcardCacha;
    [SerializeField] Image m_CachaCardImage;
    [SerializeField] Button m_LuckcardCachaBtn, m_LuckcardEnterBtn;

    [Header("AttractionPreference")]
    [SerializeField] Slider[] AttractionPreferenceSlider;
    [SerializeField] Text[] AttractionPreferenceValueText;
    [SerializeField] Slider ParkPreferenceSlider;
    [SerializeField] Text ParkPreferenceText;

    [Header("Event")]
    /// <summary>
    /// 이벤트 아이템 이미지
    /// </summary>
    [SerializeField] Sprite trash, lostObj, lostKid;
    /// <summary>
    /// 줍기용 머리 오브젝트
    /// </summary>
    [SerializeField] Image g_PickUp;

    public bool night = false;

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
            m_DateText.transform.DOScale(Vector3.one*1.2f, 1f).SetLoops(2, LoopType.Yoyo);
            m_LuckcardCacha.SetActive(true);
            GameManager.Instance.m_optionOpen = true;
            m_LuckcardEnterBtn.interactable = false;
            night = false;
        }
        else
        {
            m_TimeText.transform.DOScale(Vector3.one * 1.2f, 1f).SetLoops(2, LoopType.Yoyo);
            m_LuckcardCacha.SetActive(false);
            OptionUI.SetActive(false);
        }

        OptionNum = 0;
    }

    private void Update()
    {
        m_ParkReputation.fillAmount = ParkReputationManager.Instance.parkReputation / 1000;
        m_DateText.text = "Day " + GameValueManager.Instance.IsDate.ToString("00");
        if (night)
        {
            m_TimeText.text =
         TimeManager.Instance.IsMin.ToString("<color=red>00</color>") +
         "<color=red> : </color>" +
         TimeManager.Instance.IsSec.ToString("<color=red>00</color>");

        }
        else
        {
            m_TimeText.text = TimeManager.Instance.IsMin.ToString("00") + " : " + TimeManager.Instance.IsSec.ToString("00");
        }

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
    /// 아이템줍기
    /// </summary>
    /// <param name="argItemCode">아이템 코드</param>
    public void PickUp(int argItemCode)
    {
        SoundManager.Instance.PlayEffectSound(pickUpCilp);

        PlayerController.Instance.m_PickCode = argItemCode;
        g_PickUp.gameObject.SetActive(true);
        PickUpSprite(argItemCode);
    }

    /// <summary>
    /// 아이템 내려놓기
    /// </summary>
    public void PickDown()
    {
        g_PickUp.gameObject.SetActive(false);
        QuestManager.Instance.QuestCheck(PlayerController.Instance.m_PickCode + 6);
        QuestManager.Instance.NpcQuestCheck(2, 1);
        GameValueManager.Instance.IsGold += (10 * LuckcardManager.Instance.m_luckCardDatas[LuckcardManager.Instance.todayCardNum].ScoreAffecting);
    }

    /// <summary>
    /// 아이템 주워서 인벤토리 등록
    /// </summary>
    /// <param name="argItemCode">아이템코드</param>
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
    /// 시계 이미지 바꾸기
    /// </summary>
    /// <param name="argNum">바꿀 순서</param>
    public void SetTimeSprite(int argNum)
    {
        timeImage.transform.DOScale(Vector3.one * 1.1f, 1f).SetLoops(2, LoopType.Yoyo);
        timeImage.sprite = timeSprite[argNum];
 
        if (argNum == 2)
        {
            night = true;
        }
    }

    /// <summary>
    /// 카드 가챠
    /// </summary>
    public void LuckcardCacha()
    {
        SoundManager.Instance.PlayEffectSound(catchaClip);

        LuckcardManager.Instance.RandomLuckacard();
        m_LuckcardCachaBtn.interactable = false;
        CardImage();
    }

    /// <summary>
    /// 카드 이미지 바꾸기
    /// </summary>
    public void CardImage()
    {
        m_CachaCardImage.gameObject.SetActive(true);

        m_CachaCardImage.sprite =
            LuckcardManager.Instance.todayCardSprite;

        m_LuckcardEnterBtn.interactable = true;
    }

    /// <summary>
    /// 옵션패널 바꾸기
    /// </summary>
    /// <param name="argNum">바꾸는 옵션패널넘버</param>
    public void SetOption(int argNum)
    {
        if(OptionNum != 0)
        {
            m_OptionPanel[OptionNum].SetActive(false);
        }

        m_OptionPanel[argNum].SetActive(true);
        OptionNum = argNum;

        if (argNum == 1)//자기소개
        {
            m_PlayerName.text = SaveLoadManager.Instance.m_PlayerName;
            m_OptionGoldText.text = GameValueManager.Instance.IsGold + " G";
            m_OptionDayText.text = (20 - GameValueManager.Instance.IsDate).ToString() + "일 남음";
            m_OptionDebtText.text = "현재채무 : " + GameValueManager.Instance.IsDebt;

            m_ParkRateText.text = "B.B랜드 평판: " + 
                Mathf.RoundToInt(ParkReputationManager.Instance.parkReputation / 100).ToString("0") + " 등급";
            m_ParkSlider_0.value = ParkReputationManager.Instance.parkReputation;
        }
        else if (argNum == 2) //운카드
        {
            int num = LuckcardManager.Instance.todayCardNum;
            m_OptionCardImage.sprite = LuckcardManager.Instance.m_luckCardDatas[num].CardSprite;
            m_LuckCardName.text = LuckcardManager.Instance.m_luckCardDatas[num].CardName;
            m_LuckCradDis.text = LuckcardManager.Instance.m_luckCardDatas[num].CardDisc;
        }
        else if (argNum == 3)//npc퀘스트
        {
            NpcQuestView();
        }
        else if (argNum == 4) //친밀도
        {
            NPCManager.Instance.Option();
        }
        else if (argNum == 5) // 놀이공원 운영
        {
            ParkPreferenceSlider.value = ParkReputationManager.Instance.parkReputation;
            ParkPreferenceText.text = "B.B랜드 평판: " +
                Mathf.RoundToInt(ParkReputationManager.Instance.parkReputation / 100).ToString("0") + " 등급";

            for (int i = 0; i < AttractionPreferenceManager.Instance.m_attractionPreferences.Count; i++)
            {
                AttractionPreferenceSlider[i].value = AttractionPreferenceManager.Instance.m_attractionPreferences[i];
                AttractionPreferenceValueText[i].text = AttractionPreferenceManager.Instance.m_attractionPreferences[i].ToString("00") + "%";
            }
        }
        else if (argNum == 6) // 게임설정
        {

        }
    }

    /// <summary>
    /// 퀘스트 옵션창 표시
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
    /// npc퀘스트 보기
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
    /// 나가기 버튼
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
