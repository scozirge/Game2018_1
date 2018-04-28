using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Debugger : MonoBehaviour
{
    public GameObject Go_DebugPanel;
    public GameObject Go_FPS;
    public WarningBox MyLogBox;
    public Text Text_FPS;
    //Declare these in your class
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Start()
    {
        //限制FPS在30左右
        LimitFPS30(false);
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// 除錯顯示控制
    /// </summary>
    public void ShowDebugPanel(bool _show)
    {
        Go_DebugPanel.SetActive(_show);
    }
    /// <summary>
    /// FPS顯示控制
    /// </summary>
    public void ShowFPS(bool _show)
    {
        Go_FPS.SetActive(_show);
    }
    /// <summary>
    /// 是否限制FPS
    /// </summary>
    public void LimitFPS30(bool _limit)
    {
        if (_limit)
        {
            //限制FPS在30左右
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 30;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
    }
    void FPSCalc()
    {
        if (Text_FPS == null)
            return;
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;
        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
        Text_FPS.text = string.Format("FPS:{0}", m_lastFramerate.ToString());
    }
    public void ShowLog(string _title, string _log)
    {
        //MyLogBox.ShowLog(_title, _log);
    }
    void Update()
    {
        FPSCalc();
    }
}
