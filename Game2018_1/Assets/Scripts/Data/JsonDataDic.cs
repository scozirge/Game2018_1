using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public partial class GameDictionary
{
    //敵人字典
    public static Dictionary<string, StringData> String_SkillDic;
    public static Dictionary<int, SkillData> SkillDic;
    //UI
    public static Dictionary<string, StringData> String_UIDic;

    /// <summary>
    /// 將Json資料寫入字典裡
    /// </summary>
    static void LoadJsonDataToDic()
    {
        StringDataGetter StringGetter = new StringDataGetter();
        //敵人字典
        String_SkillDic = StringGetter.GetStringData("String_Skill");
        SkillDic = new Dictionary<int, SkillData>();
        SkillData.SetData(SkillDic, "Skill");
        //UI
        String_UIDic = StringGetter.GetStringData("String_UI");

    }
}
