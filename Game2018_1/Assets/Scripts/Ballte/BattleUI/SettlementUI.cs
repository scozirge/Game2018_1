using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettlementUI : MonoBehaviour
{
    [SerializeField]
    Text WeaknessStrikeTimes_Value;
    [SerializeField]
    Text MaxComboStrikes_Value;
    [SerializeField]
    Text ShootTimes_Value;
    [SerializeField]
    Text Accuracy_Value;
    [SerializeField]
    Text Kill_Value;
    [SerializeField]
    Text Score_Value;
    [SerializeField]
    Text HighestScoring_Value;
    [SerializeField]
    Text Post_Title;
    [SerializeField]
    Text Retry_Title;
    [SerializeField]
    Text MaxComboStrikes_Title;
    [SerializeField]
    Button RetryButton;

    void OnEnable()
    {
        RetryButton.interactable = !BattleManager.IsRevived;
        Debug.Log(BattleManager.IsRevived);
    }

    public void Settle(Dictionary<string, object> _data)
    {
        MaxComboStrikes_Title.text = GameDictionary.String_UIDic["MaxComboStrikes"].GetString(Player.UseLanguage);
        Post_Title.text = GameDictionary.String_UIDic["Post"].GetString(Player.UseLanguage);
        Retry_Title.text = GameDictionary.String_UIDic["Retry"].GetString(Player.UseLanguage);

        WeaknessStrikeTimes_Value.text = string.Format("x {0}", _data["WeaknessStrikeTimes"].ToString());
        MaxComboStrikes_Value.text = string.Format("x {0}", _data["MaxComboStrikes"].ToString());
        ShootTimes_Value.text = string.Format("x {0}", _data["ShootTimes"].ToString());
        Accuracy_Value.text = string.Format("{0}%", TextManager.ToPercent((float)_data["Accuracy"]));
        Kill_Value.text = string.Format("x {0}", _data["Kill"].ToString());
        Score_Value.text = string.Format("{0}:{1}", GameDictionary.String_UIDic["Score"].GetString(Player.UseLanguage), _data["Score"].ToString());
        HighestScoring_Value.text = string.Format("{0}:{1}", GameDictionary.String_UIDic["HighestScoring"].GetString(Player.UseLanguage), _data["HighestScoring"].ToString());

    }
    public void Restart()
    {
        gameObject.SetActive(false);
        BattleManager.ReStartGame();
    }
    public void WatchADToRevive()
    {
        BattleManager.CallAD();
        RetryButton.interactable = false;
        //gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        BattleManager.ClearGame();
        GameManager.ChangeScene("Menu");
    }
    public void Post()
    {
        Debug.Log("Post to FB");
    }
    public void ShowRank()
    {
        Debug.Log("Rank");
    }

}
