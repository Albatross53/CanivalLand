using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "ScroptableObj/NpcData", order = int.MaxValue)]

public class NpcData : ScriptableObject
{
    [SerializeField] int npcCode;
    /// <summary>
    /// npc�ڵ�
    /// </summary>
    public int NpcCode
    {
        get { return npcCode; }
    }
    [SerializeField] string npcName;
    /// <summary>
    /// npc�̸�
    /// </summary>
    public string NpcName
    {
        get { return npcName; }
    }

    [SerializeField] string npcDis;
    public string NpcDis
    {
        get { return npcDis; }
    }

    public Sprite NpcPortrait;
    public Sprite NpcIcon;
}
