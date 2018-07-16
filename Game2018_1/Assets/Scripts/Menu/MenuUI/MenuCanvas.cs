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
    LeaderboardUI MyLeaderBoard;
    [SerializeField]
    Text Start_Title;

    void Start()
    {
        if (!GameDictionary.IsInit)
            GameDictionary.InitDic();
        MyRecordUI.Init();
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
    public void CallLeaderboardUI(bool _bool)
    {
        MyLeaderBoard.gameObject.SetActive(_bool);
    }
}
