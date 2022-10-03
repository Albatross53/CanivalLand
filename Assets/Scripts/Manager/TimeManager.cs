using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// �ΰ��� �Ϸ�
    /// </summary>
    public float MaxDayTime = 5.0f;

    /// <summary>
    /// ��
    /// </summary>
    static int min = 0;
    public int IsMin
    {
        get { return min; }
        set
        {
            min = value;
            if (min < (MaxDayTime / 3))
            {
                UIManager.Instance.SetTimeSprite(0);
            }
            else if (min <= 2 * (MaxDayTime / 3))
            {
                UIManager.Instance.SetTimeSprite(1);
            }
            else
            {
                UIManager.Instance.SetTimeSprite(2);
            }

            if (min == MaxDayTime)
            {
                min = 0;
                GameManager.Instance.WorkEnd();
            }
        }
    }
    /// <summary>
    /// ��
    /// </summary>
    static int sec = 0;
    public int IsSec
    {
        get { return sec; }
        set
        {
            sec = value;
            if(sec > 60)
            {
                sec = 0;
                IsMin++;
            }
        }
    }
    /// <summary>
    /// �۷ι�ȭ
    /// </summary>
    static TimeManager g_timeManager;

    private void Awake()
    {
        if(Instance == null)
        {
            g_timeManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        InvokeRepeating("Timer", 0f, 1f);
    }

    /// <summary>
    /// 1f�� 1�ʾ� ���� �� �ؽ�Ʈ ǥ��
    /// </summary>
    void Timer()
    {
        IsSec++;
    }

    public static TimeManager Instance
    {
        get { return g_timeManager; }
    }
}
