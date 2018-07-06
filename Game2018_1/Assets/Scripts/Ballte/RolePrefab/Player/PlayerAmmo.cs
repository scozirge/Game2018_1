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
        EffectEmitter.EmitParticle("trail_shiny", Vector3.zero, Vector3.zero, transform);
        BattleManager.SetRecord("ShootTimes", 1, Operator.Plus);
    }
    protected override void OnTriggerEnter2D(Collider2D _col)
    {
        base.OnTriggerEnter2D(_col);
        switch (_col.gameObject.tag)
        {
            case "LeftCol":
                EffectEmitter.EmitParticle("bounceEffect", transform.position, new Vector3(0, 0, 180), null);
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x * -1, MyRigi.velocity.y);
                break;
            case "RightCol":
                EffectEmitter.EmitParticle("bounceEffect", transform.position, Vector3.zero, null);
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x * -1, MyRigi.velocity.y);
                break;
            case "TopCol":
                EffectEmitter.EmitParticle("bounceEffect", transform.position, new Vector3(0, 0, 90), null);
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x, MyRigi.velocity.y * -1);
                break;
            case "BotCol":
                EffectEmitter.EmitParticle("bounceEffect", transform.position, new Vector3(0, 0, 270), null);
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x, MyRigi.velocity.y * -1);
                break;
            case "EnemyShield":
                EffectEmitter.EmitParticle("shieldhit", transform.position, new Vector3(0, 0, 180 - MyMath.GetAngerFormTowPoint2D(BattleManager.MyEnemyRole.transform.position, transform.position)), null);
                BattleManager.MyEnemyRole.ShieldBeSruck(Damage);
                BattleManager.SetRecord("StrikeTimes", 1, Operator.Plus);
                SelfDestroy();
                break;
            case "Monster":
                EffectEmitter.EmitParticle("bloodEffect", transform.position, new Vector3(0, 0, 180 - MyMath.GetAngerFormTowPoint2D(BattleManager.MyEnemyRole.transform.position, transform.position)), null);
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
        EnemyRole.SetAllMonsterUnarm();
        if (IsWeaknessStrike)
            BattleManager.SetRecord("MaxComboStrikes", 1, Operator.Plus);
        else
            BattleManager.SetRecord("MaxComboStrikes", 0, Operator.Equal);
        base.SelfDestroy();
    }
}
