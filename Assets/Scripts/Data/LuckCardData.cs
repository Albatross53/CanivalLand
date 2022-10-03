using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LuckCardData", menuName = "ScroptableObj/LuckCardData", order = int.MaxValue)]
public class LuckCardData : ScriptableObject
{
    [SerializeField] int cardCode;
    /// <summary>
    /// ��ī���ڵ�
    /// </summary>
    public int CardCode { get { return cardCode; } }

    [SerializeField] string cardName;
    /// <summary>
    /// ��ī���̸�
    /// </summary>
    public string CardName { get { return cardName; } }

    [SerializeField] string cardDisc;
    /// <summary>
    /// ��ī�弳��
    /// </summary>
    public string CardDisc { get { return cardDisc; } }

    /// <summary>
    /// ��ī���̹���
    /// </summary>
    public Sprite CardSprite;

    /// <summary>
    /// �÷��̾� �ִϸ��̼�
    /// </summary>
    public Animation PlayerAnimation;

    /// <summary>
    /// �մ� �ִϸ��̼�
    /// </summary>
    public Animation CustumerAnimation;

    /// <summary>
    /// ī��� ��������
    /// </summary>
    [SerializeField] int scoreAffecting;
    public int ScoreAffecting { get { return scoreAffecting; } }

    /// <summary>
    /// ī�� ���� �̺�Ʈ
    /// </summary>
    [SerializeField] int spawnEvent;
    public int SpawnEvent { get { return spawnEvent; } }
}
