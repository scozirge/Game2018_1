using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField]
    GameObject CheckUI;

    public void CallSetting(bool _bool)
    {
        gameObject.SetActive(_bool);
    }

    public void UpdateVolume()
    {
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
