using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_ConnectCheck { get; private set; }
    static byte ReSendQuestTimes_ConnectCheck { get; set; }
    const byte MaxReSendQuestTimes_ConnectCheck = 3;
    public static void ConnectCheck()
    {
        ReSendQuestTimes_ConnectCheck = MaxReSendQuestTimes_ConnectCheck;//重置重送要求給Server的次數
        SendConnectCheckQuest();
    }
    static void SendConnectCheckQuest()
    {
        WWWForm form = new WWWForm();
        //string requestTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");//命令時間，格式2015-11-25 15:39:36
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "ConnectCheck.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_ConnectCheck = true;
        Conn.StartCoroutine(Coroutine_ConnectCheckCB(w));
        Conn.StartCoroutine(ConnectCheckTimeOutHandle(1f, 0.2f, 10));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_ConnectCheckCB(WWW w)
    {
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_ConnectCheck)
        {
            WaitCB_ConnectCheck = false;
            if (w.error == null)
            {
                try
                {
                    //////////////////成功////////////////
                    if (w.text == ServerCBCode.Success.ToString())
                    {
                        Debug.Log("connected");
                    }
                    else
                    {
                        Debug.Log("no connection");
                    }
                }
                //////////////////例外//////////////////
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }
            //////////////////回傳null////////////////
            else
            {
                Debug.Log("no connection");
            }
        }
    }
    static IEnumerator ConnectCheckTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_ConnectCheck && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_ConnectCheck)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_ConnectCheck > 0)
            {
                ReSendQuestTimes_ConnectCheck--;
                //向Server重送要求
                SendConnectCheckQuest();
            }
            else
            {
                WaitCB_ConnectCheck = false;//設定為false代表不接受回傳了
            }
        }
    }
}
