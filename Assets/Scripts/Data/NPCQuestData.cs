using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCQuestData", menuName = "ScroptableObj/NPCQuestData", order = int.MaxValue)]

public class NPCQuestData : ScriptableObject
{
    [SerializeField] int questNpcCode;
    /// <summary>
    /// 퀘스트 수주 npc넘버
    /// </summary>
    public int QuestNpcCode { get { return questNpcCode; } }

    [SerializeField] string npcQuestOrder;
    /// <summary>
    /// npc퀘스트 수주자 이름
    /// </summary>
    public string NPCQuestOrder { get { return npcQuestOrder; } }

    [SerializeField] string npcQuestName;
    /// <summary>
    /// npc퀘스트이름
    /// </summary>
    public string NPCQuestName { get { return npcQuestName; } }

    [SerializeField] [TextArea] string npcQuestDisc;
    /// <summary>
    /// npc퀘스트설명
    /// </summary>
    public string NPCQuestDisc { get { return npcQuestDisc; } }

    [SerializeField] int questType;
    /// <summary>
    /// 퀘스트 타입
    /// //0. 미니게임클리어, 1. 돈, 2. 미아, 분실물 등, 3. 시간 안에 다시 오기, 4. 인사하고오기
    /// </summary>
    public int QuestIsType { get { return questType; } }

    [SerializeField] int questMaxNum;
    /// <summary>
    /// 퀘스트에서 쓰이는 숫자값
    /// </summary>
    public int QuestMaxNum { get { return questMaxNum; } }
}
