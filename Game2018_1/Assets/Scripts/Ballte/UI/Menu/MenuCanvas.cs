using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    RecordUI MyRecordUI;
    [SerializeField]
    MenuSettingUI MySettingUI;
    [SerializeField]
    Text Start_Title;

    void Start()
    {
        if (!GameDictionary.IsInit)
            GameDictionary.InitDic();
        Start_Title.text = GameDictionary.String_UIDic["Start"].GetString(Player.UseLanguage);
    }
    public void CallRecordUI(bool _bool)
    {
        MyRecordUI.gameObject.SetActive(_bool);
    }
    public void CallSettingUI(bool _bool)
    {
        MySettingUI.gameObject.SetActive(_bool);
    }
    public void StartGame()
    {
        GameManager.ChangeScene("Battle");
    }
}
