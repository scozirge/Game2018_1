using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class BattleCanvas : MonoBehaviour
{

    [SerializeField]
    Transform Trans_Roles;
    [SerializeField]
    PlayerRoleUI PlayerRoleUIPrefab;
    [SerializeField]
    EnemyRoleUI EnemyRoleUIPrefab;

    static BattleCanvas MySelf;
    static bool IsSpawnRoles;

    static PlayerRole RelyPRole;
    static EnemyRole RelyERole;

    static PlayerRoleUI MyPlayerUI;
    static EnemyRoleUI MyEnemyUI;


    public void Init(PlayerRole _pr, EnemyRole _er)
    {
        MySelf = transform.GetComponent<BattleCanvas>();
        RelyPRole = _pr;
        RelyERole = _er;
    }

    public static void StartGame()
    {
        MySelf.SpawnRoles();
        //UpdateHealthUI & SetPos
        UpdatePlayerHealth();
        UpdateEnemyHealth();
        EnemySetPos();
        //Update Level
        UpdateLevel();
    }
    public static void ReStartGame()
    {
        ClearRoles();
        MySelf.SpawnRoles();
        //UpdateHealthUI & SetPos
        UpdatePlayerHealth();
        UpdateEnemyHealth();
        EnemySetPos();
    }

    void SpawnRoles()
    {
        //Spawn PlayerRoleUI
        GameObject playerGo = Instantiate(PlayerRoleUIPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        MyPlayerUI = playerGo.GetComponent<PlayerRoleUI>();
        playerGo.transform.SetParent(Trans_Roles);
        playerGo.transform.localPosition = RelyPRole.transform.position;
        MyPlayerUI.Init();

        //Spawn EnemyRoleUI
        GameObject enemyGo = Instantiate(EnemyRoleUIPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        MyEnemyUI = enemyGo.GetComponent<EnemyRoleUI>();
        enemyGo.transform.SetParent(Trans_Roles);
        enemyGo.transform.localPosition = RelyERole.transform.position;
        MyEnemyUI.Init();
        IsSpawnRoles = true;
    }
    public static void UpdatePlayerHealth()
    {
        if (!IsSpawnRoles)
            return;
        MyPlayerUI.UpdateHealthUI(RelyPRole.HealthRatio);
    }
    public static void UpdateEnemyHealth()
    {
        if (!IsSpawnRoles)
            return;
        MyEnemyUI.UpdateHealthUI(RelyERole.HealthRatio);
    }
    public static void EnemyShieldRotate()
    {
        if (!IsSpawnRoles)
            return;
        MyEnemyUI.RotateShield(RelyERole.ShieldAngle);
    }
    public static void PlayerSetPos()
    {
        if (!IsSpawnRoles)
            return;
        MyEnemyUI.SetPosition(RelyPRole.transform.position);
    }
    public static void EnemySetPos()
    {
        if (!IsSpawnRoles)
            return;
        MyEnemyUI.SetPosition(RelyERole.transform.position);
    }
    public static void PlayerBowDraw(float _angle, float _force)
    {
        MyPlayerUI.BowDraw(_angle, _force);
    }
    public static void ClearRoles()
    {
        MyEnemyUI.SelfDestroy();
        MyPlayerUI.SelfDestroy();
        IsSpawnRoles = false;
    }
    static void UpdateLevel()
    {
        MySelf.Level_Text.text = string.Format("Level:{0}", BattleManager.Level.ToString());
    }
}
