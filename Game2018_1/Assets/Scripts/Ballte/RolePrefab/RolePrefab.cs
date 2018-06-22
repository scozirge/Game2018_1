using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class RolePrefab : MonoBehaviour
{

    public bool IsAlive { get; protected set; }
    protected Camera MyCamera;
    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0)
                value = 0;
            health = value;
        }
    }
    int MaxHealth;
    public float HealthRatio { get { return (float)Health / (float)MaxHealth; } set { return; } }
    private int attack;
    public int Attack
    {
        get { return attack; }
        set
        {
            if (value < 0)
                value = 0;
            attack = value;
        }
    }


    public virtual void Init(Dictionary<string, object> _dataDic)
    {
        IsAlive = true;
        Health = (int)_dataDic["Health"];
        MyCamera = _dataDic["Camera"] as Camera;
        Attack = (int)_dataDic["Attack"];
        MaxHealth = Health;
    }


    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }
    public virtual void BeStruck(int _dmg)
    {
        ReceiveDmg(_dmg);
    }
    public virtual void ReceiveDmg(int _dmg)
    {
        if (!IsAlive)
            return;
        Health -= _dmg;
        DeathCheck();
    }
    protected void DeathCheck()
    {
        if (Health <= 0)
        {
            IsAlive = false;
            BattleManager.Win();
        }
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
