using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DialogueData[] questStart;
    [SerializeField] DialogueData[] questClear;
    [SerializeField] DialogueData[] nomal;

    int nowSentenceNum = 0;
    List<string> NowSentence;

    [SerializeField] Image portrait;
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject dialoguePanel;

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

    /// <summary>
    /// ��ȭ ����
    /// </summary>
    /// <param name="argCode">���� �ڵ�</param>
    /// <param name="argName"> ���� �̸�</param>
    /// <param name="argFriendship"> ���� ģ�е�</param>
    /// <param name="argSprite">���� �ʻ�ȭ</param>
    public void DialogueStart(int argCode, string argName, float argFriendship, Sprite argSprite)
    {
        dialoguePanel.SetActive(true);
        NomalSentence(argCode, argFriendship);
        SetName(argName);
        QuestSentence(argCode);
        SetPortrait(argSprite);
    }

    /// <summary>
    /// ��ȭ���� ���� �̸� ����
    /// </summary>
    /// <param name="argName">��ȭ���� ���� �̸�</param>
    void SetName(string argName)
    {
        nameText.text = argName;
    }

    /// <summary>
    /// �ʻ�ȭ ����
    /// </summary>
    /// <param name="argSprite">���� �ʻ�ȭ �̹���</param>
    void SetPortrait(Sprite argSprite)
    {
        portrait.sprite = argSprite;
    }

    /// <summary>
    /// ����Ʈ ��ȭ ���
    /// </summary>
    /// <param name="argCode">���� �ڵ�</param>
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

                QuestManager.Instance.NpcQuestNum++;
                QuestManager.Instance.NpcQuestCurNum = 0;
            }
        }

        SetDialogue();
    }

    /// <summary>
    /// ��ȭâ ����
    /// </summary>
    void SetDialogue()
    {
        dialogueText.text = NowSentence[nowSentenceNum];
    }

    /// <summary>
    /// �븻 ��� ���
    /// </summary>
    /// <param name="argCode">���� �ڵ�</param>
    /// <param name="argFirendShip">���� ģ�е�</param>
    void NomalSentence(int argCode, float argFirendShip)
    {
        NowSentence = new List<string>();
        // ģ�е��� ���� ��� �߰��ؾ���
        int num = 0;
        int ran = Random.Range(0, nomal[num].Sentence.Length);
        NowSentence.Add(nomal[num].Sentence[ran]);
        NPCManager.Instance.AddFriendship(argCode);
    }

    /// <summary>
    /// ��ȭ �ѱ��
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
            dialoguePanel.SetActive(false);
        }
    }

    public static DialogueManager Instance
    {
        get { return g_dialogueManager; }
    }
}
