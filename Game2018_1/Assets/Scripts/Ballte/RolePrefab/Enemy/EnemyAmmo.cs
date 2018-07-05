using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmo : AmmoPrefab
{
    [SerializeField]
    float LifeTime;

    protected float AngularVlocity = 3f;
    protected float Radius { get; set; }
    protected float StartRadian;
    protected float CurRadian;
    protected Vector3 ShootPos;
    public override void Init(Dictionary<string, object> _dic)
    {
        base.Init(_dic);
        ShootPos = (Vector3)_dic["ShooterPos"];
    }
    public void SetCircularMotion(float _radius, float _startAngle)
    {
        Radius = _radius;
        StartRadian = _startAngle;
        CurRadian = StartRadian;
    }
    protected override void Update()
    {
        if (BattleManager.IsPause)
            return;
        base.Update();
        CircularMotion();
        LifeTimeCountDown();
    }
    protected override void OnTriggerEnter2D(Collider2D _col)
    {
        base.OnTriggerEnter2D(_col);
        switch (_col.gameObject.tag)
        {
            case "Player":
                EffectEmitter.EmitParticle("bloodEffect", transform.position, new Vector3(0, 0, 180 - MyMath.GetAngerFormTowPoint2D(BattleManager.MyPlayerRole.transform.position, transform.position)), null);
                BattleManager.MyPlayerRole.BeStruck(Damage);
                SelfDestroy();
                break;
            case "PlayerShield":
                EffectEmitter.EmitParticle("shieldhit", transform.position, new Vector3(0, 0, 180 - MyMath.GetAngerFormTowPoint2D(BattleManager.MyPlayerRole.transform.position, transform.position)), null);
                BattleManager.MyPlayerRole.ShieldBeStruck();
                SelfDestroy();
                break;
            default:
                break;
        }
    }

    void LifeTimeCountDown()
    {
        if (!IsLaunching)
            return;
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            SelfDestroy();
    }
    void CircularMotion()
    {
        if (IsLaunching)
            return;
        CurRadian += AngularVlocity * Time.deltaTime;
        float x = Radius * Mathf.Cos(CurRadian) + ShootPos.x;
        float y = Radius * Mathf.Sin(CurRadian) + ShootPos.y;
        transform.position = new Vector2(x, y);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 - MyMath.GetAngerFormTowPoint2D(ShootPos, transform.position)));
    }
    public override void Launch()
    {
        base.Launch();
        EffectEmitter.EmitParticle("trail_arrow", Vector3.zero, Vector3.zero, transform);
        Force = (transform.position - ShootPos).normalized * 30000;
        MyRigi.AddForce(Force);
    }
}
