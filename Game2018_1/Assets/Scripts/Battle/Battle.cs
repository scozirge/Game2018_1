using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{

    [SerializeField]
    MonsterSpawner MyMonsterSpawner;
    // Use this for initialization
    void Start()
    {
        MyMonsterSpawner.SpawnMonster(5, 1);
    }
    void BattleStart()
    {

    }
}
