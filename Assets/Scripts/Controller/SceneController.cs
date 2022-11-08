using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// ĵ����
    /// </summary>
    public GameObject m_Canvas = null;
    /// <summary>
    /// �ε��г�
    /// </summary>
    public GameObject m_LoadingPanel = null;
    /// <summary>
    /// �ε��г� �����̴�
    /// </summary>
    public Slider m_LoadingSlider = null;
    /// <summary>
    /// �ε��� �ؽ�Ʈ
    /// </summary>
    public Text m_LoadingText = null;

    /// <summary>
    /// ����Ʈ�ѷ��� �۷ι�ȭ
    /// </summary>
    static SceneController g_SceneContoller = null;

    private void Awake()
    {
        if (Instance == null)
        {
            g_SceneContoller = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObj();
    }

    /// <summary>
    /// �ε�â�� �ʿ��� ������Ʈ ã��
    /// </summary>
    public void FindObj()
    {
        m_Canvas = GameObject.Find("Canvas");
        //m_Canvas = this.gameObject;
        m_LoadingPanel = m_Canvas.transform.Find("LoadingPanel").gameObject;
        m_LoadingSlider = m_LoadingPanel.transform.Find("LoadingSlider").GetComponent<Slider>();
        m_LoadingText = m_LoadingSlider.transform.Find("LoadingText").GetComponent<Text>();
    }

    public static SceneController Instance
    {
        get
        {
            return g_SceneContoller;
        }
    }

    /// <summary>
    /// ���̵� �Լ�
    /// </summary>
    /// <param name="SceneName">�̵��� �� �̸�</param>
    public void LoadScene(string SceneName)
    {
        StartCoroutine(LoadAsynchronously(SceneName));
        m_LoadingPanel.SetActive(false);
    }

    IEnumerator LoadAsynchronously(string SceneName)
    {
        AsyncOperation Operation = SceneManager.LoadSceneAsync(SceneName);

        m_LoadingPanel.SetActive(true);

        while (!Operation.isDone)
        {
            float Progress = Mathf.Clamp01(Operation.progress / .9f);
            m_LoadingSlider.value = Progress;
            m_LoadingText.text = Progress * 100f + "%";

            yield return null;
        }
    }
    /// <summary>
    /// ���ӳ�����
    /// </summary>
    public void GameExit()
    {
        Application.Quit();
    }
}
