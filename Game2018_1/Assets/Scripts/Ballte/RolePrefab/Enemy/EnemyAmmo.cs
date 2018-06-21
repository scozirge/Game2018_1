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

    public override void Init(Vector3 _shooterPos)
    {
        base.Init(_shooterPos);
    }
    public void SetCircularMotion(float _radius, float _startAngle)
    {
        Radius = _radius;
        StartRadian = _startAngle;
        CurRadian = StartRadian;
    }
    protected override void Update()
    {
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
                Destroy(gameObject);
                CameraPrefab.DoEffect("Blood");
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
    public override void Shoot()
    {
        base.Shoot();
        Force = (transform.position - ShootPos).normalized * 30000;
        MyRigi.AddForce(Force);
    }
}
