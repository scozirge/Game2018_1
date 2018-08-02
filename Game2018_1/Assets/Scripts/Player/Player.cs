using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Player
{
    public static Language UseLanguage { get; private set; }
    public static string AC { get; private set; }
    public static string ACPass { get; private set; }
    public static string Name { get; private set; }
    public static int BestScore { get; protected set; }
    public static int Kills { get; protected set; }
    public static float Accuracy { get { return MyMath.Calculate_ReturnFloat(CriticalHit, Shot, Operator.Divided); } }
    public static int Shot { get; protected set; }
    public static int CriticalHit { get; protected set; }
    public static int Death { get; protected set; }
    public static int CriticalCombo { get; protected set; }
    public static string LeaderboardData { get; protected set; }
    public static int Rank { get; private set; }


    public static string FBID { get; private set; }

    public static void Init()
    {
        //PlayerPrefs.DeleteAll();//清除玩家資料
        Player.SetLanguage(Language.EN);
        if (PlayerPrefs.GetString("AC") != "")
            AC = PlayerPrefs.GetString("AC");
        if (PlayerPrefs.GetString("ACPass") != "")
            ACPass = PlayerPrefs.GetString("ACPass");
        if (PlayerPrefs.GetString("Name") != "")
            Name = PlayerPrefs.GetString("Name");
        if (PlayerPrefs.GetInt("BestScore") != 0)
            BestScore = PlayerPrefs.GetInt("BestScore");
        if (PlayerPrefs.GetInt("Kills") != 0)
            Kills = PlayerPrefs.GetInt("Kills");
        if (PlayerPrefs.GetInt("Shot") != 0)
            Shot = PlayerPrefs.GetInt("Shot");
        if (PlayerPrefs.GetInt("CriticalHit") != 0)
            CriticalHit = PlayerPrefs.GetInt("CriticalHit");
        if (PlayerPrefs.GetInt("Death") != 0)
            Death = PlayerPrefs.GetInt("Death");
        if (PlayerPrefs.GetInt("CriticalCombo") != 0)
            CriticalCombo = PlayerPrefs.GetInt("CriticalCombo");
        if (PlayerPrefs.GetString("LeaderboardData") != "")
            LeaderboardData = PlayerPrefs.GetString("LeaderboardData");
        if (PlayerPrefs.GetString("FBID") != "")
            FBID = PlayerPrefs.GetString("FBID");
    }
    public static void UpdateRecord(Dictionary<string, object> _data)
    {
        int score = (int)_data["Score"];
        if (score > BestScore)
            BestScore = score;
        Kills += (int)_data["Kill"];
        Shot += (int)_data["ShootTimes"];
        CriticalHit += (int)_data["WeaknessStrikeTimes"];
        Death++;
        CriticalCombo += (int)_data["MaxComboStrikes"];
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("Kills", Kills);
        PlayerPrefs.SetInt("Shot", Shot);
        PlayerPrefs.SetInt("CriticalHit", CriticalHit);
        PlayerPrefs.SetInt("Death", Death);
        PlayerPrefs.SetInt("CriticalCombo", CriticalCombo);
        ServerRequest.Settlement();//資料送server
    }
    public static void AutoLogin()
    {
        //如果本地有儲存障密的話
        if (Player.AC == null || Player.ACPass == null)
        {
            ServerRequest.QuickSignUp();
        }
        else
        {
            ServerRequest.SignIn();
        }
    }
    public static void SetFBUserID(string _id)
    {
        FBID = _id;
        PlayerPrefs.SetString("FBID", FBID);
        ServerRequest.FBLogin();
    }
    public static void Test()
    {
        BestScore = 10;
        Kills = 11;
        Shot = 12;
        CriticalHit = 13;
        Death = 14;
        CriticalCombo = 15;
        ServerRequest.Settlement();
    }
}
