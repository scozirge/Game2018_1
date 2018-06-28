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
                SpawnParticleOnPos("burstblood1");
                BattleManager.MyPlayerRole.BeStruck(Damage);
                SelfDestroy();
                break;
            case "PlayerShield":
                SpawnParticleOnPos("burstblood1");
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
    }
    public override void Launch()
    {
        base.Launch();
        Force = (transform.position - ShootPos).normalized * 30000;
        MyRigi.AddForce(Force);
    }
}
