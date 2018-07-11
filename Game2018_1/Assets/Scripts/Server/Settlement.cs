using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_Settlement { get; private set; }
    static byte ReSendQuestTimes_Settlement { get; set; }
    const byte MaxReSendQuestTimes_Settlement = 2;
    public static void Settlement()
    {
        //判斷若還沒登入要先進行登入
        if (!Player.IsRigister)
        {
            GameManager.AutoLogin();
            return;
        }
        ReSendQuestTimes_Settlement = MaxReSendQuestTimes_Settlement;//重置重送要求給Server的次數
        SendSettlementQuest();
    }
    static void SendSettlementQuest()
    {
        WWWForm form = new WWWForm();
        //string requestTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");//命令時間，格式2015-11-25 15:39:36
        form.AddField("AC", Player.AC);
        form.AddField("ACPass", Player.ACPass);
        form.AddField("Score", Player.BestScore);
        form.AddField("Kills", Player.Kills);
        form.AddField("Shot", Player.Shot);
        form.AddField("CriticalHit", Player.CriticalHit);
        form.AddField("Death", Player.Death);
        form.AddField("CriticalCombo", Player.CriticalCombo);
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "Settlement.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_Settlement = true;
        Conn.StartCoroutine(Coroutine_SettlementCB(w));
        Conn.StartCoroutine(SettlementTimeOutHandle(2f, 0.5f, 5));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_SettlementCB(WWW w)
    {
        if (ReSendQuestTimes_Settlement == MaxReSendQuestTimes_Settlement)
            if (ShowLoading) CaseLogManager.ShowCaseLog(30003);//登入中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_Settlement)
        {
            WaitCB_Settlement = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == ServerCBCode.Success.ToString())
                    {
                        //string[] data = result[1].Split(',');
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
    static IEnumerator SettlementTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_Settlement && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_Settlement)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_Settlement > 0)
            {
                ReSendQuestTimes_Settlement--;
                if (ShowLoading) CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendSettlementQuest();
            }
            else
            {
                WaitCB_Settlement = false;//設定為false代表不接受回傳了
                if (ShowLoading) CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}