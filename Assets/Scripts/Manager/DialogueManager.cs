using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] AudioClip startDialog;

    [SerializeField] DialogueData[] questStart;
    [SerializeField] DialogueData[] questClear;
    [SerializeField] DialogueData[] nomal;

    int nowSentenceNum = 0;
    List<string> NowSentence;

    [SerializeField] Image portrait;
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject dialoguePanel;

    public bool isDial = false;

    static DialogueManager g_dialogueManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_dialogueManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf == true)
        {
            GameManager.Instance.m_optionOpen = true;
        }
        else if (dialoguePanel.activeSelf == false)
        {
            GameManager.Instance.m_optionOpen = false;
        }
    }

    /// <summary>
    /// 대화 시작
    /// </summary>
    /// <param name="argCode">직원 코드</param>
    /// <param name="argName"> 직원 이름</param>
    /// <param name="argFriendship"> 직원 친밀도</param>
    /// <param name="argSprite">직원 초상화</param>
    public void DialogueStart(int argCode, string argName, float argFriendship, Sprite argSprite)
    {
        SoundManager.Instance.PlayEffectSound(startDialog);

        dialoguePanel.SetActive(true);
        NomalSentence(argCode, argFriendship);
        SetName(argName);
        QuestSentence(argCode);
        SetPortrait(argSprite);
        isDial = true;
    }

    /// <summary>
    /// 대화중인 직원 이름 열기
    /// </summary>
    /// <param name="argName">대화중인 직원 이름</param>
    void SetName(string argName)
    {
        nameText.text = argName;
    }

    /// <summary>
    /// 초상화 열기
    /// </summary>
    /// <param name="argSprite">직원 초상화 이미지</param>
    void SetPortrait(Sprite argSprite)
    {
        portrait.sprite = argSprite;
    }

    /// <summary>
    /// 퀘스트 대화 출력
    /// </summary>
    /// <param name="argCode">직원 코드</param>
    public void QuestSentence(int argCode)
    {
        int num = QuestManager.Instance.NpcQuestNum;

        if (QuestManager.Instance.m_npcQuestData[num].QuestNpcCode == argCode)
        {
            if (!QuestManager.Instance.m_NpcQuestIsStart[num])
            {
                
                NowSentence = new List<string>(questStart[num].Sentence);
                QuestManager.Instance.m_NpcQuestIsStart[num] = true;
            }
            else if (QuestManager.Instance.m_NpcQuestIsClear[num])
            {
                NowSentence = new List<string>(questClear[num].Sentence);
                NPCManager.Instance.AddFriendshipQuestClear(argCode);

                GameValueManager.Instance.IsGold +=
                    QuestManager.Instance.m_questData[QuestManager.Instance.NpcQuestNum].QuestReward;

                QuestManager.Instance.NpcQuestNum++;
                QuestManager.Instance.NpcQuestCurNum = 0;
            }
        }
        else if (QuestManager.Instance.m_npcQuestData[num].QuestIsType > 4)
        {
            if((QuestManager.Instance.m_npcQuestData[num].QuestIsType - 4) == argCode)
            {
                NowSentence.Add(nomal[2].Sentence[QuestManager.Instance.m_npcQuestData[num].QuestIsType - 4]);
            }
        }

        SetDialogue();
    }

    /// <summary>
    /// 대화창 열기
    /// </summary>
    void SetDialogue()
    {
        dialogueText.text = NowSentence[nowSentenceNum];
    }

    /// <summary>
    /// 노말 대사 출력
    /// </summary>
    /// <param name="argCode">직원 코드</param>
    /// <param name="argFirendShip">직원 친밀도</param>
    void NomalSentence(int argCode, float argFirendShip)
    {
        NowSentence = new List<string>();

        if (argFirendShip < 50)
        {
            int ran = Random.Range(0, nomal[0].Sentence.Length);
            NowSentence.Add(nomal[0].Sentence[ran]);
        }
        else
        {
            int ran = Random.Range(0, nomal[1].Sentence.Length);
            NowSentence.Add(nomal[1].Sentence[ran]);
        }

        NPCManager.Instance.AddFriendship(argCode);
    }

    /// <summary>
    /// 대화 넘기기
    /// </summary>
    public void DialogueNext()
    {
        if(NowSentence.Count - 1 > nowSentenceNum)
        {
            nowSentenceNum++;
            SetDialogue();
        }
        else
        {
            nowSentenceNum = 0;
            isDial = false;
            dialoguePanel.SetActive(false);
        }
    }

    public static DialogueManager Instance
    {
        get { return g_dialogueManager; }
    }
}
