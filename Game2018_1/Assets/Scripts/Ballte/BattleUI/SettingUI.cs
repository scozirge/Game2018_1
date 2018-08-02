using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    Toggle Sound_Toggle;
    [SerializeField]
    GameObject CheckUI;


    void OnEnable()
    {
        Sound_Toggle.isOn = AudioPlayer.IsMute;
    }
    public void UpdateSoundSwitch()
    {
        AudioPlayer.Mute(Sound_Toggle.isOn);
    }
    public void CallSetting(bool _bool)
    {
        gameObject.SetActive(_bool);
    }
    public void QuitGame()
    {
        BattleManager.ClearGame();
        GameManager.ChangeScene("Menu");
    }
    public void CallQuitCheckUI(bool _bool)
    {
        CheckUI.SetActive(_bool);
    }

}
