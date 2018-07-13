using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_FBBinding { get; private set; }
    static byte ReSendQuestTimes_FBBinding { get; set; }
    const byte MaxReSendQuestTimes_FBBinding = 2;

    public static void FBBinding()
    {
        ReSendQuestTimes_FBBinding = MaxReSendQuestTimes_FBBinding;//重置重送要求給Server的次數
        SendFBBindingQuest();
    }
    static void SendFBBindingQuest()
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
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "FBBinding.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_FBBinding = true;
        Conn.StartCoroutine(Coroutine_FBBindingCB(w));
        Conn.StartCoroutine(FBBindingTimeOutHandle(2f, 0.5f, 5));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_FBBindingCB(WWW w)
    {
        if (ReSendQuestTimes_FBBinding == MaxReSendQuestTimes_FBBinding)
            CaseLogManager.ShowCaseLog(30003);//登入中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_FBBinding)
        {
            WaitCB_FBBinding = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == "UpdateAC")//server找到FBID 且跟現在本基帳號依樣 將現在資料跟FBID存到server
                    {
                        Player.UpdateACFB_CallBack();
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else if (result[0] == "ChangeAC")//server找到FBID 但跟本基帳號不一樣 依造FB帳號的server資料傳到本機
                    {
                        string[] data = result[1].Split(',');
                        Player.ChangeACFB_CallBack(data);
                    }
                    else if (result[0] == "BlindingFB")//server找不到FBID 但找到相同帳號 將FBID跟現在資料存到servery上
                    {
                        Player.BlindingFB_CallBack();
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else if (result[0] == "NoAC")//server找不到FBID 也找不到相同帳號 創立新帳號在server並將資料跟FBID傳到server上
                    {
                        string[] data = result[1].Split(',');
                        Player.NoACFB_CallBack(data);
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
    static IEnumerator FBBindingTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_FBBinding && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_FBBinding)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_FBBinding > 0)
            {
                ReSendQuestTimes_FBBinding--;
                CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendFBBindingQuest();
            }
            else
            {
                WaitCB_FBBinding = false;//設定為false代表不接受回傳了
                CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}
