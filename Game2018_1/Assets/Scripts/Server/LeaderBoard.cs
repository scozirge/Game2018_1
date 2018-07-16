using UnityEngine;
using System.Collections;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static bool WaitCB_Leaderboard { get; private set; }
    static byte ReSendQuestTimes_Leaderboard { get; set; }
    const byte MaxReSendQuestTimes_Leaderboard = 2;
    //排行榜上次更新時間
    static DateTime LastUpdateTime_Leaderboard { get; set; }
    public static void GetLeaderboard()
    {
        if (!CheckLastUpdateTime())//最快每10秒才能抓一次排行榜
            return;
        ReSendQuestTimes_Leaderboard = MaxReSendQuestTimes_Leaderboard;//重置重送要求給Server的次數
        SendLeaderboardQuest();
    }
    static void SendLeaderboardQuest()
    {
        WWWForm form = new WWWForm();
        //string requestTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");//命令時間，格式2015-11-25 15:39:36
        form.AddField("AC", Player.AC);
        form.AddField("ACPass", Player.ACPass);
        WWW w = new WWW(string.Format("{0}{1}", GetServerURL(), "Leaderboard.php"), form);
        //設定為正等待伺服器回傳
        WaitCB_Leaderboard = true;
        Conn.StartCoroutine(Coroutine_LeaderboardCB(w));
        Conn.StartCoroutine(LeaderboardTimeOutHandle(2f, 0.5f, 5));
    }
    /// <summary>
    /// 設定上次更新排行榜的時間(Client)
    /// </summary>　
    static bool CheckLastUpdateTime()
    {
        bool pass = false;
        DateTime nowTime = DateTime.Now;
        TimeSpan diffTimeSpan = nowTime - LastUpdateTime_Leaderboard;
        //如果距離上次更新超過10秒
        if (diffTimeSpan.TotalSeconds > 1)
        {
            //紀錄上次點更新的時間
            LastUpdateTime_Leaderboard = DateTime.Now;
            pass = true;
        }
        return pass;
    }
    /// <summary>
    /// 註冊回傳
    /// </summary>
    static IEnumerator Coroutine_LeaderboardCB(WWW w)
    {
        if (ReSendQuestTimes_Leaderboard == MaxReSendQuestTimes_Leaderboard)
            CaseLogManager.ShowCaseLog(30003);//登入中
        yield return w;
        Debug.LogWarning(w.text);
        if (WaitCB_Leaderboard)
        {
            WaitCB_Leaderboard = false;
            if (w.error == null)
            {
                try
                {
                    string[] result = w.text.Split(':');
                    //////////////////成功////////////////
                    if (result[0] == ServerCBCode.Success.ToString())
                    {
                        string rankStr = result[1];
                        LeaderboardUI.GetChampionData(rankStr);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    //////////////////失敗///////////////
                    else if (result[0] == ServerCBCode.Fail.ToString())
                    {
                        int caseID = int.Parse(result[1]);
                        //CaseLogManager.ShowCaseLog(caseID);
                        Debug.LogWarning("取得排行榜資料失敗");
                        LeaderboardUI.GetChampionData(Player.LeaderboardData);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                    else
                    {
                        //CaseLogManager.ShowCaseLog(2004);
                        Debug.LogWarning("取得排行榜資料失敗");
                        LeaderboardUI.GetChampionData(Player.LeaderboardData);
                        PopupUI.HideLoading();//隱藏Loading
                    }
                }
                //////////////////例外//////////////////
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    Debug.LogWarning("取得排行榜資料失敗");
                    LeaderboardUI.GetChampionData(Player.LeaderboardData);
                    //CaseLogManager.ShowCaseLog(2003);//註冊例外
                    PopupUI.HideLoading();//隱藏Loading
                }
            }
            //////////////////回傳null////////////////
            else
            {
                Debug.LogWarning(w.error);
                Debug.LogWarning("取得排行榜資料失敗");
                LeaderboardUI.GetChampionData(Player.LeaderboardData);
                //CaseLogManager.ShowCaseLog(2); ;//連線不到server
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
    static IEnumerator LeaderboardTimeOutHandle(float _firstWaitTime, float _perWaitTime, byte _checkTimes)
    {
        yield return new WaitForSeconds(_firstWaitTime);
        byte checkTimes = _checkTimes;
        //經過_fristWaitTime時間後，每_perWaitTime檢查一次資料是否回傳了，若檢查checkTimes次數後還是沒回傳就重送資料
        while (WaitCB_Leaderboard && checkTimes > 0)
        {
            checkTimes--;
            yield return new WaitForSeconds(_perWaitTime);
        }
        if (WaitCB_Leaderboard)//如果還沒接收到CB就重送需求
        {
            //若重送要求的次數達到上限次數則代表連線有嚴重問題，直接報錯
            if (ReSendQuestTimes_Leaderboard > 0)
            {
                ReSendQuestTimes_Leaderboard--;
                CaseLogManager.ShowCaseLog(30001);//連線逾時，嘗試重複連線請玩家稍待
                //向Server重送要求
                SendLeaderboardQuest();
            }
            else
            {
                WaitCB_Leaderboard = false;//設定為false代表不接受回傳了
                Debug.LogWarning("取得排行榜資料失敗");
                LeaderboardUI.GetChampionData(Player.LeaderboardData);
                //CaseLogManager.ShowCaseLog(40001); ;//請玩家檢查網路狀況或一段時間再嘗試連線
                //CaseLogManager.ShowCaseLog(11);//請玩家檢查網路狀況或一段時間再嘗試連線
                PopupUI.HideLoading();//隱藏Loading
            }
        }
    }
}