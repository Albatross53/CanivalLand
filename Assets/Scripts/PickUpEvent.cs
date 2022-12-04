using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEvent : MonoBehaviour
{
    [SerializeField] GameObject mark;

    /// <summary>
    /// 0: lostkid, 1: lostobj, 2: transh
    /// </summary>
    public int EventCode;

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
            if (!GameManager.Instance.m_optionOpen)
            {
                if (PlayerController.Instance.isEvent == false)
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
                            if (PlayerController.Instance.isEvent == false)
                            {
                                PickUp();
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
                        if (hit.collider.name == gameObject.name || hit.collider.name == "Player")
                        {
                            if (PlayerController.Instance.isEvent == false)
                            {
                                PickUp();
                            }

                        }
                    }

                }
#endif

            }
        }
        else
        {
            mark.SetActive(false);
            return;
        }
    }

    /// <summary>
    /// 아이템 줍기
    /// </summary>
    void PickUp()
    {
        UIManager.Instance.PickUp(EventCode);
        gameObject.transform.position = new Vector2(100, 100);
        PlayerController.Instance.isEvent = true;
    }
}
