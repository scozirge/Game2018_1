using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{

    public static bool IsInit { get; protected set; }
    [SerializeField]
    Debugger DebuggerPrefab;
    [SerializeField]
    PopupUI PopUIPrefab;
    [SerializeField]
    GoogleADManager GoogleAdmobPrefab;
    [SerializeField]
    FBManager FBPrefab;
    [SerializeField]
    ServerRequest SR;
    void Awake()
    {
        Screen.fullScreen = true;
    }
    void Start()
    {
        if (!Debugger.IsSpawn)
            DeployDebugger();
        if (!PopupUI.IsInit)
            DeployPopupUI();
        if (!GameDictionary.IsInit)
            GameDictionary.InitDic();
        if (!GoogleADManager.IsInit)
            DeployGoogleAdmob();
        if (!FBManager.IsSpawn)
            DeployFacebook();
        if (IsInit)
            return;
        Player.Init();
        DontDestroyOnLoad(gameObject);
        SR.Init();
        Player.AutoLogin();
        IsInit = true;
    }
    void DeployDebugger()
    {
        GameObject go = Instantiate(DebuggerPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.position = Vector3.zero;
    }
    void DeployPopupUI()
    {
        GameObject go = Instantiate(PopUIPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.position = Vector3.zero;
        PopupUI ppui = go.GetComponent<PopupUI>();
        ppui.Init();
    }
    void DeployGoogleAdmob()
    {
        GameObject googleAdMobGo = Instantiate(GoogleAdmobPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        googleAdMobGo.transform.position = Vector3.zero;
    }
    void DeployFacebook()
    {
        GameObject FacebookGo = Instantiate(FBPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        FacebookGo.transform.position = Vector3.zero;
    }
}
