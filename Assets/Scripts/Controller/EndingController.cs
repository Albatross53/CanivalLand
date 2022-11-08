using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingController : MonoBehaviour
{
    [SerializeField] Text endingCode;
    [SerializeField] Text endingName;
    [SerializeField] Text endingDis;

    [SerializeField] AudioClip endingBack;

    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.FindObj();
        SoundManager.Instance.PlayBackSound(endingBack);

        if (SaveLoadManager.Instance.m_IsFirst)
        {
            GetText(0);
        }
        else if (SaveLoadManager.Instance.m_Debt <= 0)
        {
            GetText(2);
        }
        else if (SaveLoadManager.Instance.m_Gold < SaveLoadManager.Instance.m_Debt)
        {
            GetText(1);
        }
    }

    void GetText(int argCode)
    {
        endingCode.DOText("Code: " + (EndingManager.Instance.m_endingDatas[argCode].EndingCode + 1) + ".", 3f);
        endingName.DOText("엔딩이름: " + EndingManager.Instance.m_endingDatas[argCode].EndingName, 6f);
        endingDis.DOText(EndingManager.Instance.m_endingDatas[argCode].EndingDisc, 8f);
    }

    public void GameEnding()
    {
        SceneController.Instance.LoadScene("Title");
    }

}
