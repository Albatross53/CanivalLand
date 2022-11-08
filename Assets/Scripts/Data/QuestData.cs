using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScroptableObj/QuestData", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    /// <summary>
    /// 퀘스트코드
    /// </summary>
    [SerializeField] int questCode;
    public int QuestCode { get { return questCode; } }

    /// <summary>
    /// 퀘스트 이름
    /// </summary>
    [SerializeField] string questName;
    public string QuestName { get { return questName; } }

    /// <summary>
    /// 퀘스트설명
    /// </summary>
    [SerializeField] string questDisc;
    public string QuestDisc { get { return questDisc; } }

    /// <summary>
    /// 퀘스트보상
    /// </summary>
    [SerializeField] int questReward;
    public int QuestReward { get { return questReward; } }

}
