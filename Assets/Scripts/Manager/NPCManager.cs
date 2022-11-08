using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ȣ���� ����� �ε� NpcController�ҷ����� ����ȣ���� �ɼ�â ǥ��
/// </summary>
public class NPCManager : MonoBehaviour
{
    /// <summary>
    /// ���ξ� �� NpcController
    /// </summary>
    public NpcController[] npcList;

    /// <summary>
    /// ���� ó�� ��� ����
    /// </summary>
    public List<bool> npcFirstTalkList = new List<bool>();

    /// <summary>
    /// ���� ��ȭ ����
    /// </summary>
    List<bool> npcFirstTalkToDay;

    /// <summary>
    /// ������ �ε�� ���� ȣ����
    /// </summary>
    List<float> npcFriendShipList = new List<float>();

    /// <summary>
    /// �ΰ��� ���� ȣ����
    /// </summary>
    //List<float> AddNpcFriendShipList = new List<float>();

    /// <summary>
    /// �ɼ�â ���� �ʻ�ȭ �̹���
    /// </summary>
    [SerializeField] Image[] npcIcon;
    /// <summary>
    /// �ɼ�â ���� ȣ���� ǥ�� �̹���
    /// </summary>
    [SerializeField] Image[] friendshipSlider;
    /// <summary>
    /// ���� �̸� �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text[] npcNameText;
    /// <summary>
    /// ���� ���� �ؽ�Ʈ
    /// </summary>
    [SerializeField] Text[] npcDisText;
    
    /// <summary>
    /// �۷ι�ȭ
    /// </summary>
    static NPCManager g_npcManager;

    private void Awake()
    {
        //�̱���
        if(Instance == null)
        {
            g_npcManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ���� ȣ���� �ɼ�â ǥ��
    /// </summary>
    public void Option()
    {
        for(int i = 0; i < npcList.Length; i++)
        {
            if(npcFirstTalkList[i] == true)
            {
                npcIcon[i].sprite = npcList[i].npcData.NpcIcon;
                friendshipSlider[i].fillAmount = npcFriendShipList[i] / 100;
                npcNameText[i].text = npcList[i].npcData.NpcName;
                npcDisText[i].text = npcList[i].npcData.NpcDis;
            }
            else
            {
                npcIcon[i].sprite = null;
                friendshipSlider[i].fillAmount = 0;
                npcNameText[i].text = "?";
                npcDisText[i].text = "?";
            }
        }
    }

    /// <summary>
    /// ���� ȣ���� ����
    /// </summary>
    [ContextMenu("NPCFriendshipSave")]
    public void NPCFriendshipSave()
    {
        SaveLoadManager.Instance.m_NpcfirstTalkList = new List<bool>(npcFirstTalkList);
        //npcFriendShipList = new List<float>(AddNpcFriendShipList);
        SaveLoadManager.Instance.m_NpcFriendshipList = new List<float>(npcFriendShipList);
    }

    /// <summary>
    /// ���� ȣ���� �ε� �� �Ϸ� ��ȭ �ʱ�ȭ
    /// </summary>
    public void NPCFriendshipLoad()
    {
        npcFirstTalkList = new List<bool>(SaveLoadManager.Instance.m_NpcfirstTalkList);
        npcFriendShipList = new List<float>(SaveLoadManager.Instance.m_NpcFriendshipList);
        //AddNpcFriendShipList = new List<float>(SaveLoadManager.Instance.m_NpcFriendshipList);
        for (int i = 0; i < npcFriendShipList.Count; i++)
        {
            npcList[i].isFriendship = npcFriendShipList[i];
        }

        npcFirstTalkToDay = new List<bool>();
        for (int i = 0; i < npcList.Length; i++)
        {
            npcFirstTalkToDay.Add(false);
        }
    }

    /// <summary>
    /// ���� ��ȭ ȣ���� �߰�
    /// </summary>
    /// <param name="argCode">�߰��� �����ڵ�</param>
    public void AddFriendship(int argCode)
    {
        
        if(npcFirstTalkToDay[argCode] == false)
        {
            npcFirstTalkList[argCode] = true;
            npcFirstTalkToDay[argCode] = true;
            npcList[argCode].isFriendship += 1;
            npcFriendShipList[argCode] += 1;
            QuestManager.Instance.NpcQuestCheck(4, 1);
            QuestManager.Instance.NpcQuestCheck((argCode + 5), 1);
            //flag ȣ���� ���(���ϴ�ȭ)
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// ����Ʈ Ŭ���� �� ȣ���� �߰�
    /// </summary>
    /// <param name="argCode">�߰��� ���� �ڵ�</param>
    public void AddFriendshipQuestClear(int argCode)
    {
        npcList[argCode].isFriendship += 10;
        npcFriendShipList[argCode] += 10;
        //flag ���� ȣ���� ���(����Ʈ)
    }

    public static NPCManager Instance
    {
        get { return g_npcManager; }
    }
}
