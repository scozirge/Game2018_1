using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Debugger : MonoBehaviour
{


    // Update is called once per frame
    void KeyDetector()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BattleManager.MyEnemyRole.ReceiveDmg(50);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            BattleManager.SetPause(true);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            BattleManager.SetPause(false);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            BattleManager.MyPlayerRole.ReceiveDmg(10);
        }
    }
}
