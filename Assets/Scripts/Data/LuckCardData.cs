using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LuckCardData", menuName = "ScroptableObj/LuckCardData", order = int.MaxValue)]
public class LuckCardData : ScriptableObject
{
    [SerializeField] int cardCode;
    /// <summary>
    /// 운카드코드
    /// </summary>
    public int CardCode { get { return cardCode; } }

    [SerializeField] string cardName;
    /// <summary>
    /// 운카드이름
    /// </summary>
    public string CardName { get { return cardName; } }

    [SerializeField] string cardDisc;
    /// <summary>
    /// 운카드설명
    /// </summary>
    public string CardDisc { get { return cardDisc; } }

    /// <summary>
    /// 운카드이미지
    /// </summary>
    public Sprite CardSprite;

    /// <summary>
    /// 플레이어 애니메이션
    /// </summary>
    public Animation PlayerAnimation;

    /// <summary>
    /// 손님 애니메이션
    /// </summary>
    public Animation CustumerAnimation;

    /// <summary>
    /// 카드당 연관점수
    /// </summary>
    [SerializeField] int scoreAffecting;
    public int ScoreAffecting { get { return scoreAffecting; } }

    /// <summary>
    /// 카드 스폰 이벤트
    /// </summary>
    [SerializeField] int spawnEvent;
    public int SpawnEvent { get { return spawnEvent; } }
}
