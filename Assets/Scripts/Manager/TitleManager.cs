using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] AudioClip titleBack;
    [SerializeField] AudioClip effect;

    [SerializeField] Text[] SlotName;
    [SerializeField] GameObject SlotPanel;

    [SerializeField] GameObject titleImage;

    string filePath;

    static TitleManager g_TitleManager;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/PlayerData";

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
        SoundManager.Instance.PlayBackSound(titleBack);
        titleImage.transform.DOScale(Vector3.one * 1.3f, 2f).SetLoops(-1, LoopType.Yoyo);
    }

    public void GetSlotName()
    {
        SoundManager.Instance.PlayEffectSound(effect);

        SlotPanel.SetActive(true);

        for (int i = 0; i < SlotName.Length; i++)
        {
            if (!File.Exists(filePath + i))
            {
                SlotName[i].text = "���������� �����ϴ�";
            }
            else
            {
                SaveLoadManager.Instance.Load(filePath + i);
                SlotName[i].text = "�÷��̾� �̸�: " + SaveLoadManager.Instance.m_PlayerName;
            }
        }

    }

    public void SaveDelete(int num)
    {
        if (File.Exists(filePath + num))
        {
            File.Delete(filePath + num);

            for (int i = 0; i < SlotName.Length; i++)
            {
                if (!File.Exists(filePath + i))
                {
                    SlotName[i].text = "���������� �����ϴ�";
                }
                else
                {
                    SaveLoadManager.Instance.Load(filePath + i);
                    SlotName[i].text = "�÷��̾� �̸�: " + SaveLoadManager.Instance.m_PlayerName;
                }
            }
        }
    }

    /// <summary>
    /// ���̺�ε� ���ӽ���
    /// </summary>
    public void LoadGame(int argFileNum)
    {
        SaveLoadManager.Instance.LoadStart(argFileNum);

        if (SaveLoadManager.Instance.m_IsFirst)
        {
            //ó�� ���� ����
            SceneController.Instance.LoadScene("Intro");
        }
        else
        {
            SceneController.Instance.LoadScene("Dormitory");
        }
    }

    /// <summary>
    /// ���ӳ�����
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
