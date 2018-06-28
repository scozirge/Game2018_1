using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : AmmoPrefab
{
    public int MaxBounceTimes { get; protected set; }
    public int CurBounceTimes { get; protected set; }
    public int AmmoBounceDamage { get; protected set; }
    public override int Damage { get { return BaseDamage + CurBounceTimes * AmmoBounceDamage; } }
    public bool IsWeaknessStrike { get; protected set; }
    public override void Init(Dictionary<string, object> _dic)
    {
        base.Init(_dic);
        Force = (Vector3)_dic["Force"];
        MaxBounceTimes = (int)_dic["AmmoBounceTimes"];
        AmmoBounceDamage = (int)_dic["AmmoBounceDamage"];
        CurBounceTimes = 0;
        IsWeaknessStrike = false;
    }
    public override void Launch()
    {
        base.Launch();
        SpawnParticleOnSelf("bleeding1");
        BattleManager.SetRecord("ShootTimes", 1, Operator.Plus);
    }
    protected override void OnTriggerEnter2D(Collider2D _col)
    {
        base.OnTriggerEnter2D(_col);
        Rotate();
        switch (_col.gameObject.tag)
        {
            case "HCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x * -1, MyRigi.velocity.y);
                break;
            case "VCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x, MyRigi.velocity.y * -1);
                break;
            case "EnemyShield":
                BattleManager.MyEnemyRole.BeStruck(MyMath.GetNumber1DividedByNumber2(Damage, 2));
                BattleManager.SetRecord("StrikeTimes", 1, Operator.Plus);
                SelfDestroy();
                break;
            case "Monster":
                BattleManager.MyEnemyRole.BeStruck(Damage);
                BattleManager.SetRecord("WeaknessStrikeTimes", 1, Operator.Plus);
                IsWeaknessStrike = true;
                SelfDestroy();
                break;
            default:
                break;
        }
    }
    bool Bounce()
    {
        SpawnParticleOnPos("burstblood1");
        CurBounceTimes++;
        if (CurBounceTimes > MaxBounceTimes)
        {
            SelfDestroy();
            return false;
        }
        return true;
    }
    void Rotate()
    {
        MyRigi.AddTorque(300);
    }
    public override void SelfDestroy()
    {
        SpawnParticleOnPos("burstblood1");
        EnemyRole.SetAllMonsterUnarm();
        if (IsWeaknessStrike)
            BattleManager.SetRecord("MaxComboStrikes", 1, Operator.Plus);
        else
            BattleManager.SetRecord("MaxComboStrikes", 0, Operator.Equal);
        base.SelfDestroy();
    }
}
