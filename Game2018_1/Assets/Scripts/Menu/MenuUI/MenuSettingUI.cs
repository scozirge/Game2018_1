using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSettingUI : MonoBehaviour {
    [SerializeField]
    Toggle Sound_Toggle;
    [SerializeField]
    Toggle Music_Toggle;
    public void UpdateSoundSwitch()
    {
        Debug.Log(string.Format("音效關閉{0}",Sound_Toggle.isOn));
    }
    public void UpdateMusicSwitch()
    {
        Debug.Log(string.Format("音樂關閉{0}", Music_Toggle.isOn));
    }
    public void FBLogin()
    {
        if (!FBManager.IsInit)
            FBManager.Init();
        else
            FBManager.Login();
    }
}
