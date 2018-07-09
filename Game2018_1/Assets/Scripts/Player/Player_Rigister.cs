using UnityEngine;
using System.Collections;

public partial class Player
{
    /// <summary>
    /// 設定語言
    /// </summary>
    public static void SetLanguage(Language _language)
    {
        UseLanguage = _language;
    }
    public static void SignUpGetData(string[] _data)
    {
        AC = _data[0];
        ACPass = _data[1];
        BestScore = int.Parse(_data[2]);
        Kills = int.Parse(_data[3]);
        Shot = int.Parse(_data[4]);
        CriticalHit = int.Parse(_data[5]);
        Death = int.Parse(_data[6]);
        CriticalCombo = int.Parse(_data[7]);
        PlayerPrefs.SetString("AC", AC);
        PlayerPrefs.SetString("ACPass", ACPass);
    }
    public static void SignInGetData(string[] _data)
    {
        BestScore = int.Parse(_data[0]);
        Kills = int.Parse(_data[1]);
        Shot = int.Parse(_data[2]);
        CriticalHit = int.Parse(_data[3]);
        Death = int.Parse(_data[4]);
        CriticalCombo = int.Parse(_data[5]);
    }
}
