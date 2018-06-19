using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRole : MonoBehaviour {
    public virtual int Health
    {
        get
        {
            return (int)(BaseHealth);
        }
        private set { return; }
    }
    protected int BaseHealth {get;set;}

    public virtual int Attack
    {
        get
        {
            return (int)(BaseAttack );
        }
        private set { return; }
    }
    protected int BaseAttack { get; set; }
    public PlayerRole(Dictionary<string, string> _dataDic)
    {
        BaseHealth = int.Parse(_dataDic["Health"]);
        BaseAttack = int.Parse(_dataDic["Attack"]);
    }
}
