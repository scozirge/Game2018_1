using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_ChangeName { get; private set; }
    static byte ReSendQuestTimes_ChangeName { get; set; }
    const byte MaxReSendQuestTimes_ChangeName = 2;

    public static void ChangeName(string _name)
    {
        ReSendQuestTimes_ChangeName = MaxReSendQuestTimes_ChangeName;//重置重送要求給Server的次數
        SendChangeNameQuest(_name);
    }
    static void SendChangeNameQuest(string _name)
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
        form.AddField("NewName", _name);

        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "ChangeName.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_ChangeName = true;
        Conn.StartCoroutine(Coroutine_ChangeNameCB(w));
        Conn.StartCoroutine(ChangeNameTimeOutHandle(2f, 0.5f, 5, _name));
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_ChangeNameCB(WWW w)
    {
        if (ReSendQuestTimes_ChangeName == MaxReSendQuestTimes_ChangeName)
            CaseLogManager.ShowCaseLog(30003);//登入中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_ChangeName)
        {
            WaitCB_ChangeName = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == "Success1")//db創帳號
                    {
                        string[] data = result[1].Split(',');
                        Player.ChangeName1(data);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    if (result[0] == "Success2")//db已經有帳號只更新名稱
                    {
                        string[] data = result[1].Split(',');
                        Player.ChangeName2(data);
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
    static IEnumerator ChangeNameTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes,string _name)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_ChangeName && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_ChangeName)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_ChangeName > 0)
            {
                ReSendQuestTimes_ChangeName--;
                CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendChangeNameQuest(_name);
            }
            else
            {
                WaitCB_ChangeName = false;//設定為false代表不接受回傳了
                CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}
