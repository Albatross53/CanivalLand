using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickDownEvent : MonoBehaviour
{
    [SerializeField] GameObject mark;

    /// <summary>
    /// 플레이어 레이어마스크
    /// </summary>
    public LayerMask Player;

    /// <summary>
    /// 플레이어 접촉
    /// </summary>
    bool m_IsContact = false;

    private void Update()
    {
        m_IsContact = Physics2D.OverlapCircle(transform.position, 0.7f, Player);

        if (m_IsContact)
        {
            if(PlayerController.Instance.isEvent == true)
            {
                mark.SetActive(true);
            }

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    if (hit.collider.name == gameObject.name || hit.collider.name == "Player")
                    {
                        if (PlayerController.Instance.isEvent == true)
                        {

                            PickDown();
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

                if (hit.collider != null)
                {
                    if (EventType == 0)
                    {
                        PickUp();
                    }
                    else if (EventType == 1)
                    {
                        if (PlayerController.Instance.m_PickCode == EventCode)
                        {
                            PickDown();
                            //flag 이벤트처리
                        }
                    }
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

    /// <summary>
    /// 아이템 내려놓기
    /// </summary>
    void PickDown()
    {
        UIManager.Instance.PickDown();
        EventManager.Instance.isEvent = false;
        PlayerController.Instance.isEvent = false;
    }
}
