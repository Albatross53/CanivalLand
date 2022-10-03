using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperCarManager : MonoBehaviour
{
    public int attractionsCode;

    private void Start()
    {
        SceneController.Instance.FindObj();
    }

    public void EndGame()
    {
        GameValueManager.Instance.IsWorkimg = attractionsCode;
        QuestManager.Instance.NpcQuestCheck(0, 1);
        SceneController.Instance.LoadScene("MainGame");
    }
}
