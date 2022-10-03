using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TitleManager : MonoBehaviour
{
    [SerializeField] AudioClip back;
    [SerializeField] AudioClip effect;

    [SerializeField] Text[] SlotName;
    [SerializeField] GameObject SlotPanel;

    string filePath;
    static TitleManager g_TitleManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_TitleManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.PlayBackSound(back);
    }

    public void GetSlotName()
    {
        SoundManager.Instance.PlayEffectSound(effect);

        SlotPanel.SetActive(true);
        filePath = Application.persistentDataPath + "/PlayerData";

        for (int i = 0; i < SlotName.Length; i++)
        {
            if (!File.Exists(filePath + i))
            {
                SlotName[i].text = "저장파일이 없습니다";
            }
            else
            {
                SaveLoadManager.Instance.Load(filePath + i);
                SlotName[i].text = "플레이어 이름: " + SaveLoadManager.Instance.m_PlayerName;
            }

        }

    }

    public void SaveDelete(int num)
    {
        filePath = Application.persistentDataPath + "/PlayerData";
        if (File.Exists(filePath + num))
        {
            File.Delete(filePath + num);

            SlotName[num].text = "저장파일이 없습니다";
            return;
        }
    }

    /// <summary>
    /// 세이브로드 게임시작
    /// </summary>
    public void LoadGame(int argFileNum)
    {
        SaveLoadManager.Instance.LoadStart(argFileNum);
        SceneController.Instance.LoadScene("Dormitory");
    }

    /// <summary>
    /// 게임나가기
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    public static TitleManager Instance
    {
        get { return g_TitleManager; }
    }
}
