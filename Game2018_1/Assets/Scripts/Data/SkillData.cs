using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class SkillData
{

    static string DataName;
    public int ID { get; private set; }
    //值業名稱
    public string Name
    {
        get
        {
            if (!GameDictionary.String_SkillDic.ContainsKey(ID.ToString()))
            {
                Debug.LogWarning(string.Format("String_Enemy表不包含{0}的文字資料", ID));
                return "NullText";
            }
            return GameDictionary.String_SkillDic[ID.ToString()].GetString(0, Player.UseLanguage);
        }
        private set { return; }
    }
    public string Description
    {
        get
        {
            if (!GameDictionary.String_SkillDic.ContainsKey(ID.ToString()))
            {
                Debug.LogWarning(string.Format("String_Enemy表不包含{0}的文字資料", ID));
                return "NullText";
            }
            return GameDictionary.String_SkillDic[ID.ToString()].GetString(1, Player.UseLanguage);
        }
        private set { return; }
    }
    public int RecoverHP { get; private set; }
    public int IncreaseMaxHP { get; private set; }
    public int IncreaseDamage { get; private set; }
    public int BounceDamage { get; private set; }
    public int Shield { get; private set; }
    public int DecreaseEnemyAmmo { get; private set; }
    public int IncreaseBounceTimes { get; private set; }
    public string IconString { get; private set; }






    /// <summary>
    /// 將字典傳入，依json表設定資料
    /// </summary>
    public static void SetData(Dictionary<int, SkillData> _dic, string _dataName)
    {
        DataName = _dataName;
        string jsonStr = Resources.Load<TextAsset>(string.Format("Json/{0}", DataName)).ToString();
        JsonData jd = JsonMapper.ToObject(jsonStr);
        JsonData items = jd[DataName];
        for (int i = 0; i < items.Count; i++)
        {
            SkillData data = new SkillData(items[i]);
            int id = int.Parse(items[i]["ID"].ToString());
            _dic.Add(id, data);
        }
    }
    SkillData(JsonData _item)
    {
        try
        {
            JsonData item = _item;
            foreach (string key in item.Keys)
            {
                switch (key)
                {
                    case "ID":
                        ID = int.Parse(item[key].ToString());
                        break;
                    case "RecoverHP":
                        RecoverHP = int.Parse(item[key].ToString());
                        break;
                    case "IncreaseMaxHP":
                        IncreaseMaxHP = int.Parse(item[key].ToString());
                        break;
                    case "IncreaseDamage":
                        IncreaseDamage = int.Parse(item[key].ToString());
                        break;
                    case "BounceDamage":
                        BounceDamage = int.Parse(item[key].ToString());
                        break;
                    case "Shield":
                        Shield = int.Parse(item[key].ToString());
                        break;
                    case "DecreaseEnemyAmmo":
                        DecreaseEnemyAmmo = int.Parse(item[key].ToString());
                        break;
                    case "IncreaseBounceTimes":
                        IncreaseBounceTimes = int.Parse(item[key].ToString());
                        break;
                    case "ICON":
                        IconString = item[key].ToString();
                        break;
                    default:
                        Debug.LogWarning(string.Format("{0}表有不明屬性:{1}", DataName, key));
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public static SkillData GetRandomSkill()
    {
        if (GameDictionary.SkillDic == null)
        {
            Debug.LogWarning("尚未初始化SkillData");
            return null;
        }
        List<int> keys = new List<int>(GameDictionary.SkillDic.Keys);
        int rand = UnityEngine.Random.Range(0, keys.Count);
        return GameDictionary.SkillDic[keys[rand]];
    }
    public Sprite GetSkillICON()
    {
        return Resources.Load<Sprite>(string.Format("Images/BattleUI/{0}",IconString));
    }
}
