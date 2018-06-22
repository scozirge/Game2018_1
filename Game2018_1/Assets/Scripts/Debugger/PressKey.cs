using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Debugger : MonoBehaviour {

	
	// Update is called once per frame
	void KeyDetector () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BattleManager.MyEnemyRole.ReceiveDmg(1);
        }
	}
}
