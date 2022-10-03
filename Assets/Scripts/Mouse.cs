using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
            if (hit.collider != null)
            {
                gameObject.SetActive(false);
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
                    gameObject.SetActive(false);
                }

            }
#endif

    }
        
}
