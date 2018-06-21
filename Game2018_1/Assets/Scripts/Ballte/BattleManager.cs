using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    EnemyRole EnemyPrefab;
    [SerializeField]
    PlayerRole PlayerPrefab;
    // Use this for initialization
    void Start()
    {
        SpawnRole();
    }
    void SpawnRole()
    {
        //Spawn Enemy
        GameObject enemyGo = Instantiate(EnemyPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        EnemyRole er = enemyGo.GetComponent<EnemyRole>();
        enemyGo.transform.SetParent(transform);
        //Spawn Player
        GameObject playerGo = Instantiate(PlayerPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        PlayerRole pr = playerGo.GetComponent<PlayerRole>();
        playerGo.transform.SetParent(transform);

        //Init EnemyData
        Dictionary<string, object> enemyDataDic = new Dictionary<string, object>();
        enemyDataDic.Add("Health", 3);
        enemyDataDic.Add("Camera", MyCamera);
        enemyDataDic.Add("Target", pr);
        er.Init(enemyDataDic);
        //Init PlayerData
        Dictionary<string, object> playerDataDic = new Dictionary<string, object>();
        playerDataDic.Add("Health", 3);
        playerDataDic.Add("Camera", MyCamera);
        playerDataDic.Add("Target", er);
        pr.Init(playerDataDic);
    }

}
