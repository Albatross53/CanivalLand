using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCQuestData", menuName = "ScroptableObj/NPCQuestData", order = int.MaxValue)]

public class NPCQuestData : ScriptableObject
{
    [SerializeField] int questNpcCode;
    /// <summary>
    /// ����Ʈ ���� npc�ѹ�
    /// </summary>
    public int QuestNpcCode { get { return questNpcCode; } }

    [SerializeField] string npcQuestOrder;
    /// <summary>
    /// npc����Ʈ ������ �̸�
    /// </summary>
    public string NPCQuestOrder { get { return npcQuestOrder; } }

    [SerializeField] string npcQuestName;
    /// <summary>
    /// npc����Ʈ�̸�
    /// </summary>
    public string NPCQuestName { get { return npcQuestName; } }

    [SerializeField] [TextArea] string npcQuestDisc;
    /// <summary>
    /// npc����Ʈ����
    /// </summary>
    public string NPCQuestDisc { get { return npcQuestDisc; } }

    [SerializeField] int questType;
    /// <summary>
    /// ����Ʈ Ÿ��
    /// //0. �̴ϰ���Ŭ����, 1. ��, 2. �̾�, �нǹ� ��, 3. �ð� �ȿ� �ٽ� ����, 4. �λ��ϰ����
    /// </summary>
    public int QuestIsType { get { return questType; } }

    [SerializeField] int questMaxNum;
    /// <summary>
    /// ����Ʈ���� ���̴� ���ڰ�
    /// </summary>
    public int QuestMaxNum { get { return questMaxNum; } }
}
