using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Player
{
    public static int BestScore { get; protected set; }
    public static int Kills { get; protected set; }
    public static float Accuracy { get; protected set; }
    public static int Shot { get; protected set; }
    public static int CriticalHit { get; protected set; }
    public static int Death { get; protected set; }
    public static int CriticalCombo { get; protected set; }

    public static void Init(Dictionary<string,object> _data)
    {
        Player.SetLanguage(Language.ZH_TW);
        BestScore = (int)_data["BestScore"];
        Kills = (int)_data["Kills"];
        Accuracy = (float)_data["Accuracy"];
        Shot = (int)_data["Shot"];
        CriticalHit = (int)_data["CriticalHit"];
        Death = (int)_data["Death"];
        CriticalCombo = (int)_data["CriticalCombo"];
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
            case "Accuracy":
                Accuracy = MyMath.Calculate_ReturnFloat(Accuracy, _value, _operator);
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
