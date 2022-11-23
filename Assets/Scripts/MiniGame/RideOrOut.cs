using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideOrOut : MonoBehaviour
{
    [SerializeField] bool canRide = false;
    [SerializeField] float speed = 3.0f;

    void Update()
    {
        if (canRide)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
