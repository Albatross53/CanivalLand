using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    bool isMouseOpen = false;
    [SerializeField] Animator ani;


    public void gen()
    {
        gameObject.SetActive(true);
        isMouseOpen= true;
        StartCoroutine("Life");
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(5);

        if (isMouseOpen)
        {
            isMouseOpen= false;
            gameObject.SetActive(false);
            RestaurantManager.Instance.MissRat();
        }
        else
        {
            StopCoroutine("Life");
        }
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                ani.SetTrigger("die");
                RestaurantManager.Instance.CatchRat();
                StopCoroutine("Life");
                gameObject.SetActive(false);
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
                ani.SetTrigger("die");
                RestaurantManager.Instance.CatchRat();
                StopCoroutine("Life");
                gameObject.SetActive(false);
            }

        }
#endif
    }

}
