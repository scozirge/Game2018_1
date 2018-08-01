using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSettingUI : MonoBehaviour {
    [SerializeField]
    Toggle Sound_Toggle;
    [SerializeField]
    Toggle Music_Toggle;
    [SerializeField]
    InputField Name_Text;

    void OnEnable()
    {
        Name_Text.text = Player.Name;
        Sound_Toggle.isOn = AudioPlayer.IsMute;
    }
    public void UpdateSoundSwitch()
    {
        AudioPlayer.Mute(Sound_Toggle.isOn);
    }
    public void UpdateMusicSwitch()
    {
        Debug.Log(string.Format("音樂關閉{0}", Music_Toggle.isOn));
    }
    public void FBLogin()
    {
        FBManager.MyRequest = FBRequest.GetPhoto;
        if (!FBManager.IsInit)
            FBManager.Init();
        else if (!FBManager.IsLogin)
            FBManager.Login();
        else
            PopupUI.ShowWarning("Alerdy Login!", "");
    }
    public void ChangeName()
    {
        if (Name_Text.text == Player.Name)
        {
            PopupUI.ShowWarning("Fail to change name", "The name you just input is same to your old name");
            Name_Text.text = Player.Name;
            return;
        }
        else if (Name_Text.text == "")
        {
            PopupUI.ShowWarning("Fail to change name", "The name can't be empty");
            Name_Text.text = Player.Name;
            return;
        }
        ServerRequest.ChangeName(Name_Text.text);
    }
}
