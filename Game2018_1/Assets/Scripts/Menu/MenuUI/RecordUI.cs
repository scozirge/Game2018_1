using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RecordUI : MonoBehaviour
{
    static RecordUI Myself;
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
    [SerializeField]
    Image FBIcon;

    public void Init()
    {
        Myself = this;
        gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

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
        RefreshFBIcon();
    }
    public static void RefreshFBIcon()
    {
        if (FBManager.IsGetIcon)
        {
            Texture2D t = FBManager.FBICon;
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
            Myself.FBIcon.sprite = s;
            Myself.FBIcon.gameObject.SetActive(true);

            IOManager.SaveTextureAsPNG(t, string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID));
        }
        else
        {
            //FBManager.GetProfilePhoto();
            if (File.Exists(string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID)))
            {
                Myself.FBIcon.sprite = IOManager.LoadPNGAsSprite(string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID));
                Myself.FBIcon.gameObject.SetActive(true);
            }
            else
            {
            }
        }
    }
}
