using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettlementUI : MonoBehaviour
{
    [SerializeField]
    Text WeaknessStrikeTimes_Title;
    [SerializeField]
    Text WeaknessStrikeTimes_Value;
    [SerializeField]
    Text MaxComboStrikes_Title;
    [SerializeField]
    Text MaxComboStrikes_Value;
    [SerializeField]
    Text ShootTimes_Title;
    [SerializeField]
    Text ShootTimes_Value;
    [SerializeField]
    Text Accuracy_Title;
    [SerializeField]
    Text Accuracy_Value;
    [SerializeField]
    Text Kill_Title;
    [SerializeField]
    Text Kill_Value;
    [SerializeField]
    Text Score_Title;
    [SerializeField]
    Text Score_Value;
    [SerializeField]
    Text HighestScoring_Title;
    [SerializeField]
    Text HighestScoring_Value;

    public void Settle(Dictionary<string, object> _data)
    {
        WeaknessStrikeTimes_Title.text = GameDictionary.String_UIDic["WeaknessStrikeTimes"].GetString(Player.UseLanguage);
        MaxComboStrikes_Title.text = GameDictionary.String_UIDic["MaxComboStrikes"].GetString(Player.UseLanguage);
        ShootTimes_Title.text = GameDictionary.String_UIDic["ShootTimes"].GetString(Player.UseLanguage);
        Accuracy_Title.text = GameDictionary.String_UIDic["Accuracy"].GetString(Player.UseLanguage);
        Kill_Title.text = GameDictionary.String_UIDic["Kill"].GetString(Player.UseLanguage);
        Score_Title.text = GameDictionary.String_UIDic["Score"].GetString(Player.UseLanguage);
        HighestScoring_Title.text = GameDictionary.String_UIDic["HighestScoring"].GetString(Player.UseLanguage);

        WeaknessStrikeTimes_Value.text = _data["WeaknessStrikeTimes"].ToString();
        MaxComboStrikes_Value.text = _data["MaxComboStrikes"].ToString();
        ShootTimes_Value.text = _data["ShootTimes"].ToString();
        Accuracy_Value.text = string.Format("{0}%", TextManager.ToPercent((float)_data["Accuracy"]));
        Kill_Value.text = _data["Kill"].ToString();
        Score_Value.text = _data["Score"].ToString();
        HighestScoring_Value.text = _data["HighestScoring"].ToString();
    }
    public void Restart()
    {
        gameObject.SetActive(false);
        BattleManager.ReStartGame();
    }

}
