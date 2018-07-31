using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerRole : RolePrefab
{

    protected EnemyRole Target;
    [SerializeField]
    Vector2 SpawnPos;

    public int AmmoBounceDamage { get; protected set; }
    public int DecreaseEnemyAmmo { get; protected set; }
    public int ShieldLevel { get; protected set; }



    public override void Init(Dictionary<string, object> _dataDic)
    {
        base.Init(_dataDic);
        SetTarget(_dataDic["Target"] as EnemyRole);
        AmmoBounceDamage = (int)_dataDic["AmmoBounceDamage"];
        DecreaseEnemyAmmo = (int)_dataDic["DecreaseEnemyAmmo"];
        ShieldLevel = (int)_dataDic["ShieldLevel"];
        ShieldAngle = -90;
        transform.localPosition = SpawnPos;
        InitShooter();
        MyForce = Force.Player;
    }
    public override void StartConditionRefresh()
    {
        base.StartConditionRefresh();
        SetShield();
        BattleCanvas.UpdatePlayerHealth();
        SetCanShoot(true);
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
    public void SetShield()
    {
        if (ShieldLevel < 1)
        {
            Trans_Shield.gameObject.SetActive(false);
            BattleCanvas.PlayerDisArm();
            return;
        }
        Trans_Shield.gameObject.SetActive(true);
        Trans_Shield.rotation = Quaternion.Euler(new Vector3(0, 0, ShieldAngle));
        BattleCanvas.PlayerShieldRotate();
    }
    public override void ShieldBeSruck(int _dmg)
    {
        base.ShieldBeSruck(_dmg);
        ShieldLevel -= 1;
        if (ShieldLevel < 1)
        {
            ShieldLevel = 0;
            Trans_Shield.gameObject.SetActive(false);
            BattleCanvas.PlayerDisArm();
        }
    }
    public override void ReceiveDmg(int _dmg)
    {
        base.ReceiveDmg(_dmg);
        BattleCanvas.UpdatePlayerHealth();
    }
    public void GetUpgrade(SkillData _data)
    {
        if (_data.IncreaseDamage > 0)
        {
            ExtraAttack += _data.IncreaseDamage;
        }
        if (_data.IncreaseMaxHP > 0)
        {
            IncreaseMaxHP(_data.IncreaseMaxHP);
        }
        if (_data.RecoverHP > 0)
        {
            HealHP(_data.RecoverHP);
        }
        if (_data.IncreaseBounceTimes > 0)
        {
            AmmoBounceTimes += _data.IncreaseBounceTimes;
        }
        if (_data.BounceDamage > 0)
        {
            AmmoBounceDamage += _data.BounceDamage;
        }
        if (_data.DecreaseEnemyAmmo > 0)
        {
            DecreaseEnemyAmmo += _data.DecreaseEnemyAmmo;
        }
        if (_data.Shield > 0)
        {
            ShieldLevel =1;//最高一層盾
        }
    }
    public void Revive()
    {
        IsAlive = true;
        HealHP(MaxHealth);
        BattleCanvas.UpdatePlayerHealth();
        BattleCanvas.ShowRole(MyForce, true);
    }
    protected override bool DeathCheck()
    {
        if (base.DeathCheck())
        {
            BattleManager.Lose();
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
