using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Player
{
    public static Language UseLanguage { get; private set; }
    public static string AC { get; private set; }
    public static string ACPass { get; private set; }
    public static int BestScore { get; protected set; }
    public static int Kills { get; protected set; }
    public static float Accuracy { get { return MyMath.Calculate_ReturnFloat(CriticalCombo, Shot, Operator.Divided); } }
    public static int Shot { get; protected set; }
    public static int CriticalHit { get; protected set; }
    public static int Death { get; protected set; }
    public static int CriticalCombo { get; protected set; }

    public static void Init()
    {
        //PlayerPrefs.DeleteAll();//清除玩家資料
        Player.SetLanguage(Language.ZH_TW);
        if (PlayerPrefs.GetString("AC") != "")
            AC = PlayerPrefs.GetString("AC");
        if (PlayerPrefs.GetString("ACPass") != "")
            ACPass = PlayerPrefs.GetString("ACPass");
    }
    public static void SetRecord(string _type, int _value, Operator _operator)
    {
        switch (_type)
        {
            case "BestScore":
                BestScore = MyMath.Calculate_ReturnINT(BestScore, _value, _operator);
                break;
            case "Kills":
                Kills = MyMath.Calculate_ReturnINT(Kills, _value, _operator);
                break;
            case "Shot":
                Shot = MyMath.Calculate_ReturnINT(Shot, _value, _operator);
                break;
            case "CriticalHit":
                CriticalHit = MyMath.Calculate_ReturnINT(CriticalHit, _value, _operator);
                break;
            case "Death":
                Death = MyMath.Calculate_ReturnINT(Death, _value, _operator);
                break;
            case "CriticalCombo":
                CriticalCombo = MyMath.Calculate_ReturnINT(CriticalCombo, _value, _operator);
                break;
        }
    }
}
