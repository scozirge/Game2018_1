using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{

    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public virtual int Health
    {
        get
        {
            return (int)(BaseHealth);
        }
        private set { return; }
    }
    protected int BaseHealth { get; set; }
    public virtual int Attack
    {
        get
        {
            return (int)(BaseAttack);
        }
        private set { return; }
    }
    protected int BaseAttack { get; set; }
    public string MonsterType { get; protected set; }
    public Monster(Dictionary<string, string> _dataDic)
    {
        //Name = _dataDic["Name"];
        Level = int.Parse(_dataDic["Level"]);
        BaseHealth = 50;
        BaseAttack = 10;
        //BaseHealth = int.Parse(_dataDic["Health"]);
        //BaseAttack = int.Parse(_dataDic["Attack"]);
        //MonsterType = _dataDic["MonsterType"];
    }
}
