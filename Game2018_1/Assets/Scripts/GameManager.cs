using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{

    public static bool IsInit { get; protected set; }
    [SerializeField]
    Debugger DebuggerPrefab;
    [SerializeField]
    GoogleADManager GoogleAdmobPrefab;
    void Start()
    {
        if (!Debugger.IsSpawn)
            DeployDebugger();
        if (!GameDictionary.IsInit)
            GameDictionary.InitDic();
        if (IsInit)
            return;
        GetPlayerDtata();
        DeployGoogleAdmob();
        DontDestroyOnLoad(gameObject);
        GameManager.ChangeScene("Menu");
        IsInit = true;
    }
    void DeployDebugger()
    {
        GameObject debugGo = Instantiate(DebuggerPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        debugGo.transform.position = Vector3.zero;
    }
    void DeployGoogleAdmob()
    {
        GameObject googleAdMobGo = Instantiate(GoogleAdmobPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        googleAdMobGo.transform.position = Vector3.zero;
    }
    void GetPlayerDtata()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("BestScore", 10);
        data.Add("Kills", 11);
        data.Add("Accuracy", 0.5f);
        data.Add("Shot", 13);
        data.Add("CriticalHit", 14);
        data.Add("Death", 15);
        data.Add("CriticalCombo", 16);
        Player.Init(data);
    }
}
