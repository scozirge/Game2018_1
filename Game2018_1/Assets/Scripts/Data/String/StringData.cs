using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class StringData
{
    public string ID { get; private set; }
    public string ZH_TW { get; protected set; }
    public string ZH_CN { get; protected set; }
    public string EN { get; protected set; }
    public string StringType { get; protected set; }

    public StringData(JsonData _item, string _stringType)
    {
        try
        {
            JsonData item = _item;
            StringType = _stringType;
            foreach (string key in item.Keys)
            {
                switch (key)
                {
                    case "ID":
                        ID = item[key].ToString();
                        break;
                    case "ZH-TW":
                        ZH_TW = item[key].ToString();
                        break;
                    case "ZH-CN":
                        ZH_CN = item[key].ToString();
                        break;
                    case "EN":
                        EN = item[key].ToString();
                        break;
                    default:
                        Debug.LogWarning(string.Format("{0}表有不明屬性:{1}", StringType, key));
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    /// <summary>
    /// 取得文字
    /// </summary>
    public string GetString(Language _language)
    {
        switch (_language)
        {
            case Language.ZH_TW:
                return ZH_TW;
            case Language.ZH_CN:
                return ZH_CN;
            case Language.EN:
                return EN;
            default:
                return EN;
        }
    }
    public string GetString(int _index, Language _language)
    {
        switch (_language)
        {
            case Language.ZH_TW:
                return GetComplexString(_index, ZH_TW);
            case Language.ZH_CN:
                return GetComplexString(_index, ZH_CN);
            case Language.EN:
                return GetComplexString(_index, EN);
            default:
                return GetComplexString(_index, EN);
        }
    }
    string GetComplexString(int _index, string _str)
    {
        string[] stringArray = TextManager.StringSplitToStringArray(_str, '/');
        if (stringArray.Length - 1 < _index || _index < 0)
        {
            Debug.LogWarning("取複合文字時發生錯誤的資料");
            return "nullText";
        }
        return stringArray[_index];
    }
}
