using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerRole : RolePrefab
{

    protected EnemyRole Target;
    [SerializeField]
    Vector2 SpawnPos;

    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        SetTarget(_dataDic["Target"] as EnemyRole);
        transform.localPosition = SpawnPos;
        InitShooter();
    }
    public void SetTarget(EnemyRole _enemy)
    {
        Target = _enemy;
    }
    protected override void Update()
    {
        base.Update();
        ClickToSpawn();
    }
    public override void ReceiveDmg(int _dmg)
    {
        base.ReceiveDmg(_dmg);
        BattleCanvas.UpdatePlayerHealth();
    }

}
