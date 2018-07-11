using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{

    [SerializeField]
    Text PlayerRecord_Title;
    [SerializeField]
    Text Best_Title;
    [SerializeField]
    Text Kill_Title;
    [SerializeField]
    Text Accuracy_Title;
    [SerializeField]
    Text Shot_Title;
    [SerializeField]
    Text CriticalHit_Title;
    [SerializeField]
    Text Death_Title;
    [SerializeField]
    Text CriticalCombo_Title;
    [SerializeField]
    Text Post_Title;
    [SerializeField]
    Text Back_Title;

    [SerializeField]
    Text Kill_Value;
    [SerializeField]
    Text Accuracy_Value;
    [SerializeField]
    Text Shot_Value;
    [SerializeField]
    Text CriticalHit_Value;
    [SerializeField]
    Text Death_Value;
    [SerializeField]
    Text CriticalCombo_Value;

    void OnEnable()
    {
        PlayerRecord_Title.text = GameDictionary.String_UIDic["PlayerRecord"].GetString(Player.UseLanguage);
        Best_Title.text = string.Format("{0}:{1}", GameDictionary.String_UIDic["HighestScoring"].GetString(Player.UseLanguage), Player.BestScore);
        Kill_Title.text = GameDictionary.String_UIDic["Kills"].GetString(Player.UseLanguage);
        Accuracy_Title.text = GameDictionary.String_UIDic["Accuracy"].GetString(Player.UseLanguage);
        Shot_Title.text = GameDictionary.String_UIDic["ShootTimes"].GetString(Player.UseLanguage);
        CriticalHit_Title.text = GameDictionary.String_UIDic["WeaknessStrikeTimes"].GetString(Player.UseLanguage);
        Death_Title.text = GameDictionary.String_UIDic["Dealth"].GetString(Player.UseLanguage);
        CriticalCombo_Title.text = GameDictionary.String_UIDic["MaxComboStrikes"].GetString(Player.UseLanguage);
        Post_Title.text = GameDictionary.String_UIDic["Post"].GetString(Player.UseLanguage);
        Back_Title.text = GameDictionary.String_UIDic["Back"].GetString(Player.UseLanguage);

        Kill_Value.text = Player.Kills.ToString();
        Accuracy_Value.text = string.Format("{0}%", TextManager.ToPercent(Player.Accuracy));
        Shot_Value.text = Player.Shot.ToString();
        CriticalHit_Value.text = Player.CriticalHit.ToString();
        Death_Value.text = Player.Death.ToString();
        CriticalCombo_Value.text = Player.CriticalCombo.ToString();
    }
}
