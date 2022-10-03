using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// 캔버스
    /// </summary>
    public GameObject m_Canvas = null;
    /// <summary>
    /// 로딩패널
    /// </summary>
    public GameObject m_LoadingPanel = null;
    /// <summary>
    /// 로딩패널 슬라이더
    /// </summary>
    public Slider m_LoadingSlider = null;
    /// <summary>
    /// 로딩률 텍스트
    /// </summary>
    public Text m_LoadingText = null;

    /// <summary>
    /// 씬컨트롤러를 글로벌화
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
    /// 로딩창에 필요한 오브젝트 찾기
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
    /// 씬이동 함수
    /// </summary>
    /// <param name="SceneName">이동할 씬 이름</param>
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
    /// 게임나가기
    /// </summary>
    public void GameExit()
    {
        Application.Quit();
    }
}
