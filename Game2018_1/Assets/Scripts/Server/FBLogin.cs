using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_FBLogin { get; private set; }
    static byte ReSendQuestTimes_FBLogin { get; set; }
    const byte MaxReSendQuestTimes_FBLogin = 2;

    public static void FBLogin()
    {
        ReSendQuestTimes_FBLogin = MaxReSendQuestTimes_FBLogin;//重置重送要求給Server的次數
        SendFBLoginQuest();
    }
    static void SendFBLoginQuest()
    {
        WWWForm form = new WWWForm();
        //string requestTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");//命令時間，格式2015-11-25 15:39:36
        form.AddField("AC", Player.AC);
        form.AddField("ACPass", Player.ACPass);
        form.AddField("BestScore", Player.BestScore);
        form.AddField("Kills", Player.Kills);
        form.AddField("Shot", Player.Shot);
        form.AddField("CriticalHit", Player.CriticalHit);
        form.AddField("Death", Player.Death);
        form.AddField("CriticalCombo", Player.CriticalCombo);
        form.AddField("FBID", Player.FBID);
        Debug.Log(Player.AC);
        Debug.Log(Player.FBID);
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "FBLogin.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_FBLogin = true;
        Conn.StartCoroutine(Coroutine_FBLoginCB(w));
        Conn.StartCoroutine(FBLoginTimeOutHandle(2f, 0.5f, 5));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_FBLoginCB(WWW w)
    {
        if (ReSendQuestTimes_FBLogin == MaxReSendQuestTimes_FBLogin)
            CaseLogManager.ShowCaseLog(30003);//登入中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_FBLogin)
        {
            WaitCB_FBLogin = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == "Success1")//server找到FBID 且跟現在本基帳號依樣 將現在資料跟FBID存到server
                    {
                        string[] data = result[1].Split(',');
                        Player.FBLogin_CB1(data);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else if (result[0] == "Success2")//server找到FBID 但跟本基帳號不一樣 依造FB帳號的server資料傳到本機
                    {
                        Player.FBLogin_CB2();
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    //////////////////失敗///////////////
                    else if (result[0] == ServerCBCode.Fail.ToString())
                    {
                        int caseID = int.Parse(result[1]);
                        CaseLogManager.ShowCaseLog(caseID);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else
                    {
                        CaseLogManager.ShowCaseLog(2004);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                }
                //////////////////例外//////////////////
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    CaseLogManager.ShowCaseLog(2003);//註冊例外
                    PopupUI.HideLoading();//隱藏Loading
                }
            }
            //////////////////回傳null////////////////
            else
            {
                Debug.LogWarning(w.error);
                CaseLogManager.ShowCaseLog(2); ;//連線不到server
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
    static IEnumerator FBLoginTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_FBLogin && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_FBLogin)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_FBLogin > 0)
            {
                ReSendQuestTimes_FBLogin--;
                CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendFBLoginQuest();
            }
            else
            {
                WaitCB_FBLogin = false;//設定為false代表不接受回傳了
                CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}
