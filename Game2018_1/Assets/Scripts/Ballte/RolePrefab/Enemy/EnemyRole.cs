using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyRole : RolePrefab
{
    [SerializeField]
    EnemyAmmoSpawner MyAmmoSpawner;
    [SerializeField]
    float MoveRangeX;
    [SerializeField]
    float MovePosY;

    delegate void OneDelegate();
    static OneDelegate MonsterUnarm;

    bool BeginUnarm;
    const float UnarmTime = 0.5f;
    float UnarmTimer;
    public override int AmmoNum
    {
        get
        {
            if (BaseAmmoNum - BattleManager.MyPlayerRole.DecreaseEnemyAmmo < 1)
                return 1;
            else
                return BaseAmmoNum - BattleManager.MyPlayerRole.DecreaseEnemyAmmo;
        }
    }


    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        BaseAmmoNum = (int)_dataDic["AmmoNum"];
        Move();
        MyForce = Force.Enemy;
    }
    public override void StartConditionRefresh()
    {
        base.StartConditionRefresh();
        BattleCanvas.UpdateEnemyHealth();
        BattleCanvas.EnemySetPos();
    }
    protected override void Start()
    {
        base.Start();
        MonsterUnarm += BeginToUnarm;
        BeginUnarm = false;
    }
    protected override void Update()
    {
        if (BattleManager.IsPause)
            return;
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
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("Damage", Attack);
        data.Add("ShooterPos", transform.position);
        data.Add("AmmoNum", AmmoNum);
        MyAmmoSpawner.SpawnAmmo(data);
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
    protected override bool DeathCheck()
    {
        if (base.DeathCheck())
        {
            BattleManager.SetRecord("Kill", 1, Operator.Plus);
            BattleManager.Win();
        }
        else
        {
        }
        return !IsAlive;
    }
    public override void SelfDestroy()
    {
        MyAmmoSpawner.DestroyAllAmmos();
        base.SelfDestroy();
    }
}
