using UnityEngine;
using System.Collections;

public class CaseLogManager
{
    /// <summary>
    /// 顯示Log，傳入case
    /// </summary>
    public static void ShowCaseLog(int _case)
    {
        CaseTableData caseData;
        if (GameDictionary.CaseTableDic.ContainsKey(_case))
            caseData = GameDictionary.CaseTableDic[_case];
        else
        {
            Debug.LogWarning("CaseID錯誤");
            PopupUI.ShowWarning("發生錯誤", "無此錯誤碼");
            return;
        }
        //Erro-JustClose(1~1000)
        if (_case <= 1000)
        {
            PopupUI.ShowWarning(caseData.Title, caseData.Description);
        }
        //Erro-BackToMenu(1001~2000)
        else if (_case > 1000 && _case <= 2000)
        {
            PopupUI.ShowError_BackToMenu(caseData.Title, caseData.Description);
        }
        //Erro-CloseGame(1001~2000)
        else if (_case > 2000 && _case <= 3000)
        {
            //PopupUI.ShowError(caseData.Title, caseData.Description, PopupBtnType.CloseGame);
        }
        //Tip-JustClose(10001~11000)
        else if (_case > 10000 && _case <= 11000)
        {
            //PopupUI.ShowTip(caseData.Title, caseData.Description, PopupBtnType.JustClose);
        }
        //Tip-BackToMenu(11001~12000)
        else if (_case > 11000 && _case < 12000)
        {
            return;
            //PopupUI.ShowTip(caseData.Title, caseData.Description, PopupBtnType.BackToMenu);
        }
        //Log(20001~21000)
        else if (_case > 20000 && _case <= 21000)
        {
            Debug.Log(string.Format("Title:{0}", caseData.Title));
            Debug.Log(string.Format("Description:{0}", caseData.Description));
        }
        //Loading(30001~31000)
        else if (_case > 30000 && _case <= 31000)
        {
            PopupUI.ShowLoading(caseData.Description);
        }
        //ReLogin(40001~41000)
        else if (_case > 40000 && _case <= 41000)
        {
            PopupUI.ShowWarning(caseData.Title, caseData.Description);
        }
    }
}
