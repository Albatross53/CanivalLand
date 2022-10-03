using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] int attractionPosCode;
    [SerializeField] string sceneName;
    public LayerMask playerLayer;
    bool hit = false;

    [SerializeField] GameObject mark;

    void Update()
    {
        hit = Physics2D.OverlapCircle(transform.position, 0.9f, playerLayer);

        if (hit)
        {
            mark.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null)
                {
                    if (hit.collider.name == gameObject.name)
                    {
                        if (sceneName == "Dormitory")
                        {
                            GameManager.Instance.WorkEnd();
                        }
                        else
                        {
                            GameValueManager.Instance.IsAttractionPosCode = attractionPosCode;
                            NPCManager.Instance.NPCFriendshipSave();
                            SceneController.Instance.LoadScene(sceneName);
                        }
                    }
                }
                
            }
        }
        else
        {
            mark.SetActive(false);
            return;
        }
    }


}
