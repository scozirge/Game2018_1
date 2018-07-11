using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour {

    public static bool IsInit { get; private set; }
    static PopupUI Myself;
    [SerializeField]
    GameObject LoadingGo;
    [SerializeField]
    Text Loading_Value;

    [SerializeField]
    GameObject WarningGo;
    [SerializeField]
    Text Warning_Title;
    [SerializeField]
    Text Warning_Value;

    [SerializeField]
    GameObject Error_BackToMenuGo;
    [SerializeField]
    Text Error_BackToMenu_Value;
    public void Init()
    {
        Myself = this;
        IsInit = true;
        DontDestroyOnLoad(gameObject);
    }
    public static void ShowLoading(string _text)
    {
        if (!Myself)
            return;
        Myself.LoadingGo.SetActive(true);
        Myself.Loading_Value.text = _text;
    }

    public static void HideLoading()
    {
        if (!Myself)
            return;
        Myself.LoadingGo.SetActive(false);
    }

    public static void ShowWarning(string _title,string _description)
    {
        if (!Myself)
            return;
        Myself.WarningGo.SetActive(true);
        Myself.Warning_Title.text = _title;
        Myself.Warning_Value.text = _description;
    }
    public void HideWarning()
    {
        if (!Myself)
            return;
        Myself.WarningGo.SetActive(false);
    }

    public static void ShowError_BackToMenu(string _title, string _description)
    {
        if (!Myself)
            return;
        Myself.Error_BackToMenuGo.SetActive(true);
        Myself.Error_BackToMenu_Value.text = _description;
    }
    public void HideError_BackToMenu()
    {
        if (!Myself)
            return;
        Myself.Error_BackToMenuGo.SetActive(false);
    }
}
