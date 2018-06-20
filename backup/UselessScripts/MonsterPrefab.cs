using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPrefab : MonoBehaviour
{
    Monster RelyMonster { get; set; }
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
    public int Index { get; protected set; }
    [SerializeField]
    TextMesh Health_TM;
    [SerializeField]
    Vector3 BotLeftSpawnPos;
    [SerializeField]
    float MoveDist;
    [SerializeField]
    int MaxRow;
    float IntervalTime = 1f;
    float Timer;
    public int CurPosX { get; protected set; }
    public int CurPosY { get; protected set; }

    public void Init(int _index, Monster _monster, int[] _pos)
    {
        Timer = IntervalTime;
        RelyMonster = _monster;
        Health = RelyMonster.Health;
        MaxHealth = Health;
        IsAlive = true;
        Index = _index;
        CurPosX = _pos[0];
        CurPosY = _pos[1];
        Move();
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        Health_TM.text = Health.ToString();
    }
    public void ReceiveDmg(int _dmg)
    {
        if (!IsAlive)
            return;
        Health -= _dmg;
        DeathCheck();
        UpdateHealthUI();
    }
    public void DeathCheck()
    {
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveCountDown();
    }
    void MoveCountDown()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            MoveCheck();
        }
    }
    void MoveCheck()
    {
        CurPosY--;
        if (CurPosY <= -1)
            CurPosY = MaxRow - 1;
        Move();
        Timer = IntervalTime;
    }
    void Move()
    {
        transform.localPosition = BotLeftSpawnPos + new Vector3(CurPosX * MoveDist, CurPosY * MoveDist, 0);
    }

}
