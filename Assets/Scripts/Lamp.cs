using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] GameObject lightobj;

    void Update()
    {
        if (!UIManager.Instance.night)
        {
            lightobj.SetActive(false);
        }
        else
        {
            lightobj.SetActive(true);
        }

    }
}
