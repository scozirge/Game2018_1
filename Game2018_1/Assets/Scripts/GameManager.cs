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
    ServerRequest SR;
    void Start()
    {
        if (!Debugger.IsSpawn)
            DeployDebugger();
        if (!PopupUI.IsInit)
            DeployPopupUI();
        if (!GameDictionary.IsInit)
            GameDictionary.InitDic();
        if (IsInit)
            return;
        Player.Init();
        DeployGoogleAdmob();
        DontDestroyOnLoad(gameObject);
        GameManager.ChangeScene("Menu");
        IsInit = true;
        SR.Init();
        AutoLogin();
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
    public static void AutoLogin()
    {
        //如果本地有儲存障密的話
        if (Player.AC == null || Player.ACPass == null)
        {
            ServerRequest.QuickSignUp();
        }
        else
        {
            ServerRequest.SignIn();
        }

    }
}
