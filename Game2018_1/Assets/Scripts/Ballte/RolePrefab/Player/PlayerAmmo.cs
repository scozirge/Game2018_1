using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : AmmoPrefab
{
    public int MaxBounceTimes { get; protected set; }
    public int curBounceTimes { get; protected set; }
    public override void Init(Vector3 _shooterPos, int _damage)
    {
        base.Init(_shooterPos, _damage);
        MaxBounceTimes = 3;
        curBounceTimes = 0;
    }
    public override void Launch(Vector2 _force)
    {
        base.Launch(_force);
        SpawnParticleOnSelf("bleeding1");
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
                SelfDestroy();
                break;
            case "Monster":
                _col.GetComponent<EnemyRole>().BeStruck(Damage);
                SelfDestroy();
                break;
            default:
                break;
        }
    }
    bool Bounce()
    {
        SpawnParticleOnPos("burstblood1");
        curBounceTimes++;
        if (curBounceTimes > MaxBounceTimes)
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
    protected override void SelfDestroy()
    {
        SpawnParticleOnPos("burstblood1");
        EnemyRole.SetAllMonsterUnarm();
        base.SelfDestroy();
    }
}
