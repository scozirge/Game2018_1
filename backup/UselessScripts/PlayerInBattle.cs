using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInBattle : MonoBehaviour
{
    MonsterPrefab Target;
    float AttackTimer;

    public void Init()
    {
        InitAttribute();
        InitBattleCtrl();
    }
    void Update()
    {
        AttackCountDown();
        TouchDetect();
    }

    void SetTarget(MonsterPrefab _target)
    {
        Target = _target;
        Debug.Log(Target.name);
    }
    public void AttackCountDown()
    {
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
        else
        {
            DoAttack();
        }
    }
    void DoAttack()
    {
        if (!Target)
            return;
        Target.ReceiveDmg(Attack);
        AttackTimer = AttackSpeed;
    }
}
