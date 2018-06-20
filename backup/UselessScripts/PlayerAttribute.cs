using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInBattle
{
    public bool IsAlive { get; protected set; }
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
    public int MaxHealth { get; protected set; }
    public float HealthRatio { get { return (float)Health / (float)MaxHealth; } protected set { return; } }

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
    private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    void InitAttribute()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        Attack = 10;
        AttackSpeed = 1f;
    }
}
