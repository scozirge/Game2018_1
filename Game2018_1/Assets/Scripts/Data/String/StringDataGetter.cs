using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public class StringDataGetter
{
    /// <summary>
    /// 將字典傳入，依json表設定資料
    /// </summary>
    public Dictionary<string, StringData> GetStringData(string _stringType)
    {
        string jsonStr = Resources.Load<TextAsset>(string.Format("Json/{0}", _stringType)).ToString();
        JsonData jd = JsonMapper.ToObject(jsonStr);
        JsonData items = jd[_stringType];
        Dictionary<string, StringData> dic = new Dictionary<string, StringData>();
        for (int i = 0; i < items.Count; i++)
        {
            StringData data = new StringData(items[i], _stringType);
            string name = items[i]["ID"].ToString();
            if (dic.ContainsKey(name))
            {
                Debug.LogWarning(string.Format("{0}的主屬性名稱重複", _stringType));
                break;
            }
            dic.Add(name, data);
        }
        return dic;
    }
}
