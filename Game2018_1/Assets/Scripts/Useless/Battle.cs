using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    [SerializeField]
    PlayerInBattle PIB;
    [SerializeField]
    MonsterSpawner MyMonsterSpawner;
    // Use this for initialization
    void Start()
    {
        PIB.Init();
        MyMonsterSpawner.SpawnMonster(5, 1);
    }
    void BattleStart()
    {

    }
}
