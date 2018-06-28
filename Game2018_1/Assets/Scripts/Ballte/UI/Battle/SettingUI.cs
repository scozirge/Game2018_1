using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    Slider AudioSlider;

    public void CallSetting(bool _bool)
    {
        gameObject.SetActive(_bool);
    }

    public void UpdateVolume()
    {
        AudioListener.volume = AudioSlider.value;
    }
    public void QuitGame()
    {
        GameManager.ChangeScene("Menu");
    }
}
