using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public partial class GameDictionary
{
    public static bool IsInit { get; private set; }
    /// <summary>
    /// 設定字典
    /// </summary>
    public static void InitDic()
    {
        if (IsInit)
            return;
        IsInit = true;
        //將Json資料寫入字典裡
        LoadJsonDataToDic();
    }
}
