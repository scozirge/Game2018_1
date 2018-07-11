using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour {

    public static int Level { get; private set; }
    public static void Upgrade()
    {
        BattleCanvas.CallSkillUpgrade();
    }
    public static void Settlement()
    {
        //存資料
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("WeaknessStrikeTimes", WeaknessStrikeTimes);
        data.Add("MaxComboStrikes", MaxComboStrikes);
        data.Add("ShootTimes", ShootTimes);
        data.Add("Accuracy", Accuracy);
        data.Add("Kill", Kill);
        data.Add("Score", Score);
        data.Add("HighestScoring", HighestScoring);
        Player.UpdateRecord(data);
        //
        BattleCanvas.Settle(data);
        SetPause(true);
    }
    public static void Win()
    {
        SetPause(true);
        BattleCanvas.Win();
        PlayerRole.SetCanShoot(false);
    }
    public static void Lose()
    {
        SetPause(true);
        BattleCanvas.Lose();
        PlayerRole.SetCanShoot(false);
    }
    public static void NextLevel()
    {
        Level++;
        BattleCanvas.UpdateLevel();
        BattleCanvas.NextLevel();
    }

}
