using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class BattleCanvas
{
    [SerializeField]
    PhaseUI MyPhaseUI;
    [SerializeField]
    Text Level_Text;


    public static void Win()
    {
        MySelf.MyPhaseUI.PlayMotion("Win", 0);
    }
    public static void NextLevel()
    {
        UpdateLevel();
        MySelf.MyPhaseUI.PlayMotion("NextLevel", 0);
    }
}
