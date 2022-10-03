using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viking : MonoBehaviour
{
    float speed = 35;
    bool dir = false;
    bool vikingCanTurn = true;

    static Viking g_Viking;

    public static Viking Instance
    {
        get{ return g_Viking; }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            g_Viking = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        VikingManager.Instance.BtnActive(vikingCanTurn);

        VikingTurnCheck();
        
        DirCheck();
    }

    public void VikingTurn(bool argBool)
    {
        if (argBool)
        {
            StartCoroutine("VikingTuning");
        }
        else
        {
            StopCoroutine("VikingTuning");
        }
    }

    IEnumerator VikingTuning()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
        yield return new WaitForFixedUpdate();
        StartCoroutine("VikingTuning");
    }

    /// <summary>
    ///  바이킹 회전위치 체크
    /// </summary>
    void VikingTurnCheck()
    {
        if (0.6 <= Mathf.Abs(gameObject.transform.rotation.z) && Mathf.Abs(gameObject.transform.rotation.z) < 0.8)
        {
            vikingCanTurn = true;
        }
        else
        {
            vikingCanTurn = false;
        }
    }

    /// <summary>
    /// 바이킹 방향체크
    /// </summary>
    void DirCheck()
    {
        if (dir == true)
        {
            if (0.8 <= Mathf.Abs(gameObject.transform.rotation.z))
            {
                dir = false;
            }
        }
        else if (dir == false)
        {
            if (Mathf.Abs(gameObject.transform.rotation.z) < 0.6)
            {
                dir = true;
            }
        }
    }

    /// <summary>
    /// 바이킹 회전
    /// </summary>
    public void VikingTurn()
    {
        if (speed > 0)
        {
            speed = -(Mathf.Abs(speed) + 10);
        }
        else
        {
            speed = (Mathf.Abs(speed) + 10);
        }

        VikingManager.Instance.GetReaction(dir);

        if(dir == true)
        {
            VikingManager.Instance.AddScore();
        }
    }
}
