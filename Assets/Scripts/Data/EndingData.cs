using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingData", menuName = "ScroptableObj/EndingData", order = int.MaxValue)]
public class EndingData : ScriptableObject
{
    [SerializeField] int endingCode;
    /// <summary>
    /// 엔딩코드
    /// </summary>
    public int EndingCode { get { return endingCode; } }

    [SerializeField] string endingName;
    /// <summary>
    /// 엔딩이름
    /// </summary>
    public string EndingName { get { return endingName; } }

    [SerializeField] string endingDisc;
    /// <summary>
    /// 엔딩설명
    /// </summary>
    public string EndingDisc { get { return endingDisc; } }

}
