using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField] CustomerObject poolObj;
    int allLocateCount;
    Stack<CustomerObject> stack = new Stack<CustomerObject>();

    void Start()
    {
        SetAllLoateCount();
        AllLocate();
    }

    public void AllLocate()
    {
        for (int i = 0; i < allLocateCount; i++)
        {
            CustomerObject Obj = Instantiate(poolObj);
            Obj.Creat(this);
            stack.Push(Obj);
        }
    }

    void SetAllLoateCount()
    {
        float num = ParkReputationManager.Instance.parkReputation / 100;
        if(num != 0)
        {
            allLocateCount = Mathf.RoundToInt(num) * 10;
        }
        allLocateCount = 10;
    }

    public GameObject PopObject()
    {
        CustomerObject obj = stack.Pop();
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }

    public void PushObject(CustomerObject obj)
    {
        obj.gameObject.SetActive(false);
        stack.Push(obj);
    }
}
