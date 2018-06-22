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
    int AmmoNum;


    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        AmmoNum = (int)_dataDic["AmmoNum"];
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
    public override void BeStruck(int _dmg)
    {
        base.BeStruck(_dmg);
        PlayMotion("BeStruck", 0);
        CameraPrefab.DoEffect("Blood");
        CameraPrefab.DoAction("Shake", 0);
    }
    public override void ReceiveDmg(int _dmg)
    {
        base.ReceiveDmg(_dmg);
        BattleCanvas.UpdateEnemyHealth();
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
        Vector2 movePos = new Vector2(Random.Range(-MoveRangeX, MoveRangeX), MovePosY);
        transform.position = movePos;
        BattleCanvas.EnemySetPos();
    }
    public void Arm()
    {
        Move();
        MyAmmoSpawner.SpawnAmmo(AmmoNum, transform.position, Attack);
        SetShield();
    }
    void SetShield()
    {
        Trans_Shield.gameObject.SetActive(true);
        ShieldAngle = Random.Range(0, 360);
        Trans_Shield.rotation = Quaternion.Euler(new Vector3(0, 0, ShieldAngle));
        BattleCanvas.EnemyShieldRotate();
    }
    public void LaunchAmmo()
    {
        MyAmmoSpawner.LaunchAmmo();
    }
    void UnArm()
    {
        Trans_Shield.gameObject.SetActive(false);
        BeginUnarm = false;
        PlayerRole.SetCanShoot(true);
    }
    public void BeginToUnarm()
    {
        BeginUnarm = true;
        UnarmTimer = UnarmTime;
    }


}
