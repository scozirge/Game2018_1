using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BattleManager : MonoBehaviour
{
    [SerializeField]
    BattleCanvas MyBattleCanvas;
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    EnemyRole EnemyPrefab;
    [SerializeField]
    PlayerRole PlayerPrefab;

    static BattleManager MySelf;
    public static PlayerRole MyPlayerRole;
    public static EnemyRole MyEnemyRole;

    void Start()
    {
        MySelf = transform.GetComponent<BattleManager>();
        BattleManager.StartGame();
    }
    // Use this for initialization
    public static void StartGame()
    {
        Level = 1;
        MySelf.SpawnRoles();
        MySelf.MyBattleCanvas.Init(MyPlayerRole, MyEnemyRole);
        BattleCanvas.StartGame();
    }
    public static void ReStartGame()
    {
        MySelf.ClearEnemyRole();
        MySelf.SpanwEnemyRole();
        MyPlayerRole.SetTarget(MyEnemyRole);
        MySelf.MyBattleCanvas.Init(MyPlayerRole, MyEnemyRole);
        BattleCanvas.ReStartGame();
        PlayerRole.SetCanShoot(true);

    }
    void SpanwEnemyRole()
    {
        //Spawn Enemy
        GameObject enemyGo = Instantiate(EnemyPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        MyEnemyRole = enemyGo.GetComponent<EnemyRole>();
        enemyGo.transform.SetParent(transform);
        //Init EnemyData
        Dictionary<string, object> enemyDataDic = new Dictionary<string, object>();
        enemyDataDic.Add("Health", 2);
        enemyDataDic.Add("Attack", 1);
        enemyDataDic.Add("Camera", MyCamera);
        enemyDataDic.Add("AmmoNum", Level + 3);
        MyEnemyRole.Init(enemyDataDic);
    }
    void SpawnRoles()
    {
        //Spawn Enemy
        GameObject enemyGo = Instantiate(EnemyPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        MyEnemyRole = enemyGo.GetComponent<EnemyRole>();
        enemyGo.transform.SetParent(transform);
        //Spawn Player
        GameObject playerGo = Instantiate(PlayerPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        MyPlayerRole = playerGo.GetComponent<PlayerRole>();
        playerGo.transform.SetParent(transform);

        //Init EnemyData
        Dictionary<string, object> enemyDataDic = new Dictionary<string, object>();
        enemyDataDic.Add("Health", 2);
        enemyDataDic.Add("Attack", 1);
        enemyDataDic.Add("Camera", MyCamera);
        enemyDataDic.Add("AmmoNum", Level + 3);
        MyEnemyRole.Init(enemyDataDic);
        //Init PlayerData
        Dictionary<string, object> playerDataDic = new Dictionary<string, object>();
        playerDataDic.Add("Health", 3);
        playerDataDic.Add("Attack", 1);
        playerDataDic.Add("Camera", MyCamera);
        playerDataDic.Add("Target", MyEnemyRole);
        MyPlayerRole.Init(playerDataDic);
    }
    void ClearRoles()
    {
        MyPlayerRole.SelfDestroy();
    }
    void ClearEnemyRole()
    {
        MyEnemyRole.SelfDestroy();
    }
}
