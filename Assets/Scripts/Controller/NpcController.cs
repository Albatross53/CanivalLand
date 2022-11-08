using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] GameObject mark;

    /// <summary>
    /// 직원 데이터
    /// </summary>
    public NpcData npcData;

    float m_Friendship = 0;

    /// <summary>
    /// 직원 호감도
    /// </summary>
    public float isFriendship
    {
        get { return m_Friendship; }
        set
        {
            m_Friendship = value;
            if (m_Friendship >= 100)
            {
                m_Friendship = 100;
            }
        }
    }
    public LayerMask Player;

    bool m_IsContact = false;

    /// <summary>
    /// 글로벌화
    /// </summary>
    static NpcController g_npcController;

    public static NpcController Instance
    {
        get { return g_npcController; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            g_npcController = this;
        }
    }


    private void Update()
    {
        m_IsContact = Physics2D.OverlapCircle(transform.position, 1.0f, Player);

        if (m_IsContact)
        {
            mark.SetActive(true);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    if (hit.collider.name == gameObject.name || hit.collider.name == "Player")
                    {
                        if (!DialogueManager.Instance.isDial)
                        {
                            DialogueManager.Instance.DialogueStart(npcData.NpcCode, npcData.NpcName, m_Friendship, npcData.NpcPortrait);
                        }
                    }
                }
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider == gameObject)
                {
                    DialogueManager.Instance.DialogueStart(npcData.NpcCode, npcData.NpcName, m_Friendship, npcData.NpcSprite);
                    QuestManager.Instance.NpcQuestCheck(4, 1);
                }

            }
#endif

        }
        else
        {
            mark.SetActive(false);
            return;
        }

    }
}
