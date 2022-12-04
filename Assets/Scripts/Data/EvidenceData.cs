using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceData", menuName = "ScroptableObj/EvidenceData", order = int.MaxValue)]

public class EvidenceData : ScriptableObject
{
    [SerializeField] int evidenceCode;
    /// <summary>
    /// 단서코드
    /// </summary>
    public int EvidenceCode { get { return evidenceCode; } }

    [SerializeField][TextArea] string evidenceDisc;
    /// <summary>
    /// 단서설명
    /// </summary>
    public string EvidenceDisc { get { return evidenceDisc; } }
}
