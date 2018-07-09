using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class CaseTableData
{
    public int CaseID { get; private set; }
    public string Reason { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool SendGALog { get; private set; }
    /// <summary>
    /// 將字典傳入，依json表設定資料
    /// </summary>
    public static void SetData(Dictionary<int, CaseTableData> _dic)
    {
        string jsonStr = Resources.Load<TextAsset>("Json/CaseTable").ToString();
        JsonData jd = JsonMapper.ToObject(jsonStr);
        JsonData CaseTableItems = jd["CaseTable"];
        for (int i = 0; i < CaseTableItems.Count; i++)
        {
            CaseTableData caseTableData = new CaseTableData(CaseTableItems[i]);
            int id = caseTableData.CaseID;
            _dic.Add(id, caseTableData);
        }
    }
    CaseTableData(JsonData _item)
    {
        try
        {
            JsonData item = _item;
            CaseID = int.Parse(item["CaseID"].ToString());
            Reason = item["Reason"].ToString();
            Title = item["Title"].ToString();
            Description = item["Description"].ToString();
            SendGALog = bool.Parse(item["SendGALog"].ToString());
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}