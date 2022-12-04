using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using DG.Tweening;

public class Zombi : MonoBehaviour
{
    [SerializeField] GameObject mark;

    /// <summary>
    /// 플레이어 레이어마스크
    /// </summary>
    public LayerMask Player;
    public LayerMask wall;

    /// <summary>
    /// 플레이어 접촉
    /// </summary>
    bool m_IsContact = false;

    int dir = 0;

    private void Start()
    {
        StartCoroutine("move");
        dir = 0;
    }
    private IEnumerator move()
    {
        int num = Random.Range(0, 4);
        dir = num;
        switch (num)
        {
            case 0:
                gameObject.transform.DOLocalMoveX(1, 1).SetRelative();
                this.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case 1:
                gameObject.transform.DOLocalMoveX(-1, 1).SetRelative();
                this.transform.localScale = new Vector3(1, 1, 1);
                break; 
            case 2:
                gameObject.transform.DOLocalMoveY(1, 1).SetRelative();
                break;
            case 3:
                gameObject.transform.DOLocalMoveY(-1, 1).SetRelative();
                break;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("move");
    }

    IEnumerator backMove()
    {
        switch (dir)
        {
            case 0:
                gameObject.transform.DOLocalMoveX(-1, 1).SetRelative();
                this.transform.localScale = new Vector3(1, 1, 1);
                break;
            case 1:
                gameObject.transform.DOLocalMoveX(1, 1).SetRelative();
                this.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case 2:
                gameObject.transform.DOLocalMoveY(-1, 1).SetRelative();
                break;
            case 3:
                gameObject.transform.DOLocalMoveY(1, 1).SetRelative();
                break;
        }
        yield return new WaitForSeconds(1f);

        StartCoroutine("move");
    }
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

        bool contact = Physics2D.OverlapCircle(transform.position, 1.2f, wall);
        if(contact)
        {
            StopCoroutine("move");
            StartCoroutine("backMove");
        }
    }

    /// <summary>
    /// 아이템 줍기
    /// </summary>
    void PickUp()
    {
        UIManager.Instance.PickUp(3);
        PlayerController.Instance.isEvent = true;
        LuckCardController.Instance.curEventNum--;
        Destroy(gameObject);
    }
}
