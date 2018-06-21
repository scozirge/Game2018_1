using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRole : RolePrefab
{
    [SerializeField]
    PlayerAmmoSpawner MyAmmoSpawner;
    protected EnemyRole Target;

    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        Target = _dataDic["Target"] as EnemyRole;
        MyAmmoSpawner.Init(Target, MyCamera);
    }

}
