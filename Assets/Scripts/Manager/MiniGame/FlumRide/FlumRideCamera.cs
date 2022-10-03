using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlumRideCamera : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(other.gameObject.tag == "Customer")
            {
                print("yes");
                FlumRideManager.Instance.addScore(10);
            }else if(other.gameObject.tag == "Obstruction")
            {
                print("no");
                FlumRideManager.Instance.addScore(-10);
            }
        }
    }
}
