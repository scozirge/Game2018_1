using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyRole : RolePrefab
{
    [SerializeField]
    EnemyAmmoSpawner MyAmmoSpawner;
    [SerializeField]
    Transform Trans_Shield;
    [SerializeField]
    float MoveRangeX;
    [SerializeField]
    float MovePosY;

    delegate void OneDelegate();
    static OneDelegate MonsterUnarm;
    public int ShieldAngle { get; protected set; }
    bool BeginUnarm;
    const float UnarmTime = 0.5f;
    float UnarmTimer;


    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        Move();
    }
    protected override void Start()
    {
        base.Start();
        MonsterUnarm += BeginToUnarm;
        BeginUnarm = false;
    }
    protected override void Update()
    {
        base.Update();
        CountDownToUnArm();
    }
    public override void BeStruck()
    {
        base.BeStruck();
        PlayMotion("BeStruck", 0);
        CameraPrefab.DoEffect("Blood");
        CameraPrefab.DoAction("Shake", 0);
    }
    void CountDownToUnArm()
    {
        if (!BeginUnarm)
            return;
        UnarmTimer -= Time.deltaTime;
        if (UnarmTimer <= 0)
        {
            UnArm();
        }
    }
    public static void SetAllMonsterUnarm()
    {
        if (MonsterUnarm != null)
            MonsterUnarm();
    }
    void Move()
    {
        float x = Random.Range(-MoveRangeX, MoveRangeX);
        transform.position = new Vector2(x, MovePosY);
    }
    public void Arm()
    {
        Move();
        MyAmmoSpawner.SpawnAmmo(transform.position);
        Trans_Shield.gameObject.SetActive(true);
        ShieldAngle = Random.Range(0, 360);
        Trans_Shield.rotation = Quaternion.Euler(new Vector3(0, 0, ShieldAngle));
    }
    public void LaunchAmmo()
    {
        MyAmmoSpawner.ShootAmmo();
    }
    void UnArm()
    {
        Trans_Shield.gameObject.SetActive(false);
        BeginUnarm = false;
        PlayerAmmoSpawner.SetCanShoot(true);
    }
    public void BeginToUnarm()
    {
        BeginUnarm = true;
        UnarmTimer = UnarmTime;
    }


}
