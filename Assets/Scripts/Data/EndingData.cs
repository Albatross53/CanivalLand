using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndingData", menuName = "ScroptableObj/EndingData", order = int.MaxValue)]
public class EndingData : ScriptableObject
{
    [SerializeField] int endingCode;
    /// <summary>
    /// �����ڵ�
    /// </summary>
    public int EndingCode { get { return endingCode; } }

    [SerializeField] string endingName;
    /// <summary>
    /// �����̸�
    /// </summary>
    public string EndingName { get { return endingName; } }

    [SerializeField] string endingDisc;
    /// <summary>
    /// ��������
    /// </summary>
    public string EndingDisc { get { return endingDisc; } }

}
