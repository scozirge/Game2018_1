using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public partial class ServerRequest : MonoBehaviour
{

    public static ServerRequest Conn;
    const string TestServerURL = "127.0.0.1/Game2018_1/";
    const string ServerURL = "http://game2018-1.000webhostapp.com/";
    static bool IsFormal = false;
    static bool ShowLoading = false;//是否顯示loading

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Conn = this;
        //切場景不移除物件
        DontDestroyOnLoad(gameObject);
    }
    public static string GetServerURL()
    {
        if (IsFormal)
        {
            return ServerURL;
        }
        else
            return TestServerURL;
    }

    public static void Regist()
    {
        QuickSignUp();
    }
}
