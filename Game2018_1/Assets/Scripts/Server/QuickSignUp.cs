using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{
    //快速註冊回傳
    public static bool WaitCB_QuickSignUp { get; private set; }
    //註冊需求重送要求給Server次數
    static byte ReSendQuestTimes_QuickSignUp { get; set; }
    //每次註冊需求最大重送次數，註冊預設是1次，以免太多垃圾帳戶
    const byte MaxReSendQuestTimes_QuickSignUp = 1;
    /// <summary>
    /// 註冊，傳入帳密
    /// </summary>
    public static void QuickSignUp()
    {
        ReSendQuestTimes_QuickSignUp = MaxReSendQuestTimes_QuickSignUp;//重置重送要求給Server的次數
        SendSignUpQuest();
    }
    static void SendSignUpQuest()
    {
        WWWForm form = new WWWForm();
        //string requestTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");//命令時間，格式2015-11-25 15:39:36
        form.AddField("BestScore", Player.BestScore);
        form.AddField("Kills", Player.Kills);
        form.AddField("Shot", Player.Shot);
        form.AddField("CriticalHit", Player.CriticalHit);
        form.AddField("Death", Player.Death);
        form.AddField("CriticalCombo", Player.CriticalCombo);
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "QuickSignUp.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_QuickSignUp = true;
        Conn.StartCoroutine(Coroutine_QuickSignUpCB(w));
        Conn.StartCoroutine(SignUpTimeOutHandle(2f, 0.5f, 12));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_QuickSignUpCB(WWW w)
    {
        if (ReSendQuestTimes_QuickSignUp == MaxReSendQuestTimes_QuickSignUp)
            if (ShowLoading) CaseLogManager.ShowCaseLog(30002);//註冊帳戶中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_QuickSignUp)
        {
            WaitCB_QuickSignUp = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == ServerCBCode.Success.ToString())
                    {
                        string[] data = result[1].Split(',');
                        Player.SignUpGetData(data);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    //////////////////失敗///////////////
                    else if (result[0] == ServerCBCode.Fail.ToString())
                    {
                        int caseID = int.Parse(result[1]);
                        if (ShowLoading) CaseLogManager.ShowCaseLog(caseID);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else
                    {
                        if (ShowLoading) CaseLogManager.ShowCaseLog(2004);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                }
                //////////////////例外//////////////////
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    if (ShowLoading) CaseLogManager.ShowCaseLog(2003);//註冊例外
                    PopupUI.HideLoading();//隱藏Loading
                }
            }
            //////////////////回傳null////////////////
            else
            {
                Debug.LogWarning(w.error);
                if (ShowLoading) CaseLogManager.ShowCaseLog(2); ;//連線不到server
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
    static IEnumerator SignUpTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_QuickSignUp && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_QuickSignUp)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_QuickSignUp > 0)
            {
                ReSendQuestTimes_QuickSignUp--;
                if (ShowLoading) CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendSignUpQuest();
            }
            else
            {
                WaitCB_QuickSignUp = false;//設定為false代表不接受回傳了
                if (ShowLoading) CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}
