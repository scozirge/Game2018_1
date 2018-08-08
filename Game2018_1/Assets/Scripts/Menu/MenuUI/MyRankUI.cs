using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MyRankUI : MonoBehaviour
{
    [SerializeField]
    Text Name_Text;
    [SerializeField]
    Text Score_Text;
    [SerializeField]
    Image Icon_Sprite;


    public Sprite MySprite { get; private set; }

    public void UpdateMyRank()
    {
        Name_Text.text = Player.Name;
        Score_Text.text = Player.BestScore.ToString();
        if (Player.Rank > LeaderboardUI.MaxItemNum)
            Score_Text.text = string.Format("{0}{1}{2}", GameDictionary.String_UIDic["CurrentRank"].GetString(Player.UseLanguage), MyMath.GetTopProportionInTotal(Player.Rank, ChampionData.TotalChampionNum), "%");
        else
            Score_Text.text = string.Format("{0}{1}", GameDictionary.String_UIDic["CurrentRank"].GetString(Player.UseLanguage), Player.Rank.ToString());
        if (Player.FBID != "")
        {
            FBManager.GetChampionIcon(Icon_CB());
        }
    }
    IEnumerator Icon_CB()
    {
        WWW url = new WWW(string.Format("https://graph.facebook.com/{0}/picture?type=small", Player.FBID));
        Texture2D t = new Texture2D(128, 128);
        yield return url;
        if (url.error == null)
        {
            url.LoadImageIntoTexture(t);
            MySprite = IOManager.ChangeTextureToSprite(t);
            Icon_Sprite.sprite = MySprite;
            IOManager.SaveTextureAsPNG(t, string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID));
        }
        else
        {
            GetLocalHostIcon();
        }
    }
    void GetLocalHostIcon()
    {
        if (File.Exists(string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID)))
        {
            MySprite = IOManager.LoadPNGAsSprite(string.Format("{0}/{1}", Application.persistentDataPath, Player.FBID));
            Icon_Sprite.sprite = MySprite;
        }
    }
}
