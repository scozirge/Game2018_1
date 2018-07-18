using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class BattleCanvas : MonoBehaviour
{

    [SerializeField]
    Transform Trans_Roles;
    [SerializeField]
    PlayerRoleUI PlayerRoleUIPrefab;
    [SerializeField]
    EnemyRoleUI EnemyRoleUIPrefab;
    [SerializeField]
    PhaseUI MyPhaseUI;
    [SerializeField]
    Text Level_Title;
    [SerializeField]
    Text Level_Text;
    [SerializeField]
    SettingUI MySettingUI;
    [SerializeField]
    UpgradeUI MyUpgradeUI;
    [SerializeField]
    SettlementUI MySettlementUI;
    [SerializeField]
    GameObject LeaderboardUI;



    static BattleCanvas MySelf;
    bool IsSpawnRoles;

    static PlayerRole RelyPRole;
    static EnemyRole RelyERole;

    static PlayerRoleUI MyPlayerUI;
    static EnemyRoleUI MyEnemyUI;


    void Start()
    {
        if (GameDictionary.IsInit)
            Level_Title.text = GameDictionary.String_UIDic["Round"].GetString(Player.UseLanguage);
    }
    public void Init(PlayerRole _pr, EnemyRole _er)
    {
        MySelf = transform.GetComponent<BattleCanvas>();
        RelyPRole = _pr;
        RelyERole = _er;
    }

    public static void SetStage()
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
    static bool CheckInit()
    {
        if (MySelf == null || MySelf.IsSpawnRoles == false)
            return false;
        else
            return true;
    }
    public static void UpdatePlayerHealth()
    {
        if (!CheckInit())
            return;
        MyPlayerUI.UpdateHealthUI(RelyPRole.HealthRatio);
    }
    public static void ShowRole(Force _force, bool _bool)
    {
        switch (_force)
        {
            case Force.Player:
                MyPlayerUI.gameObject.SetActive(_bool);
                break;
            case Force.Enemy:
                MyEnemyUI.gameObject.SetActive(_bool);
                break;
        }
    }
    public static void UpdateEnemyHealth()
    {
        if (!CheckInit())
            return;
        MyEnemyUI.UpdateHealthUI(RelyERole.HealthRatio);
    }
    public static void EnemyShieldRotate()
    {
        if (!CheckInit())
            return;
        MyEnemyUI.RotateShield(RelyERole.ShieldAngle);
    }
    public static void PlayerShieldRotate()
    {
        if (!CheckInit())
            return;
        MyPlayerUI.RotateShield(RelyPRole.ShieldAngle);
    }
    public static void PlayerDisArm()
    {
        if (!CheckInit())
            return;
        MyPlayerUI.Disarm();
    }
    public static void PlayerSetPos()
    {
        if (!CheckInit())
            return;
        MyEnemyUI.SetPosition(RelyPRole.transform.position);
    }
    public static void EnemySetPos()
    {
        if (!CheckInit())
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
        MySelf.IsSpawnRoles = false;
    }
    public static void UpdateLevel()
    {
        MySelf.Level_Text.text = BattleManager.Level.ToString();
    }
    public static void Win()
    {
        MySelf.MyPhaseUI.PlayMotion("Win", 0);
    }
    public static void Lose()
    {
        MySelf.MyPhaseUI.PlayMotion("Lose", 0);
    }
    public static void NextLevel()
    {
        MySelf.MyPhaseUI.PlayMotion("NextLevel", 0);
    }
    public static void StartGame()
    {
        MySelf.MyPhaseUI.PlayMotion("StartGame", 0);
    }
    public void CallSetting()
    {
        MySettingUI.CallSetting(true);
        BattleManager.SetPause(true);
    }
    public void EndCallSetting()
    {
        MySettingUI.CallSetting(false);
        BattleManager.SetPause(false);
    }
    public static void CallSkillUpgrade()
    {
        MySelf.MyUpgradeUI.gameObject.SetActive(true);
        MySelf.MyUpgradeUI.SetSkillBoard();
    }
    public static void EndCallSkillBoard()
    {
        MySelf.MyUpgradeUI.gameObject.SetActive(false);
    }
    public static void Settle(Dictionary<string, object> _data)
    {
        MySelf.MySettlementUI.gameObject.SetActive(true);
        MySelf.MySettlementUI.Settle(_data);
    }
    public void CallLeaderBoardUI(bool _bool)
    {
        LeaderboardUI.SetActive(_bool);
    }
}
