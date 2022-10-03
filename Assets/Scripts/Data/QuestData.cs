using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScroptableObj/QuestData", order = int.MaxValue)]
public class QuestData : ScriptableObject
{
    /// <summary>
    /// ����Ʈ�ڵ�
    /// </summary>
    [SerializeField] int questCode;
    public int QuestCode { get { return questCode; } }

    /// <summary>
    /// ����Ʈ �̸�
    /// </summary>
    [SerializeField] string questName;
    public string QuestName { get { return questName; } }

    /// <summary>
    /// ����Ʈ����
    /// </summary>
    [SerializeField] string questDisc;
    public string QuestDisc { get { return questDisc; } }

    /// <summary>
    /// ����Ʈ����
    /// </summary>
    [SerializeField] int questReward;
    public int QuestReward { get { return questReward; } }

}
