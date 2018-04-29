using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FPSController : MonoBehaviour
{
    Text Text_FPS;
    //Declare these in your class
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Text_FPS = transform.Find("fps").GetComponent<Text>();
        //限制FPS在40左右
        //QualitySettings.vSyncCount = 0;  // VSync must be disabled
        //Application.targetFrameRate = 35;
    }

    void Update()
    {
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
}
