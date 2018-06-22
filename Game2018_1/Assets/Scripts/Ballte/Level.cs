using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour {

    public static int Level { get; private set; }
    public static void InitLevel()
    {

    }
    public static void Settlement()
    {
        
    }
    public static void Win()
    {
        BattleCanvas.Win();
        PlayerRole.SetCanShoot(false);
    }
    public static void NextLevel()
    {
        Level++;
        BattleCanvas.NextLevel();
    }

}
