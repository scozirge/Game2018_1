using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField]
    Text Name_Text;
    [SerializeField]
    Text Score_Text;
    [SerializeField]
    Image Icon_Sprite;
    [SerializeField]
    Text Rank_Text;
    [SerializeField]
    Image RankBot_Image;

    [SerializeField]
    Sprite Rank1Bot_Prefab;
    [SerializeField]
    Sprite Rank2Bot_Prefab;
    [SerializeField]
    Sprite Rank3Bot_Prefab;
    [SerializeField]
    Sprite Rank4Bot_Prefab;

    public Sprite MySprite { get; private set; }

    ChampionData MyChampionData;

    public void Initialize(ChampionData _data)
    {
        MyChampionData = _data;
        Name_Text.text = MyChampionData.Name;
        Score_Text.text = MyChampionData.Score.ToString();
        /*
         *         if (MyChampionData.Rank > LeaderboardUI.MaxItemNum)
            Rank_Text.text = string.Format("{0}{1}{2}", GameDictionary.String_UIDic[""].GetString(Player.UseLanguage), 1, "%");
        else
         */

        Rank_Text.text = MyChampionData.Rank.ToString();
        switch (MyChampionData.Rank)
        {
            case 1:
                RankBot_Image.sprite = Rank1Bot_Prefab;
                break;
            case 2:
                RankBot_Image.sprite = Rank2Bot_Prefab;
                break;
            case 3:
                RankBot_Image.sprite = Rank3Bot_Prefab;
                break;
            default:
                RankBot_Image.sprite = Rank4Bot_Prefab;
                break;
        }
        if (MyChampionData.FBID != "")
        {
            FBManager.GetChampionIcon(Icon_CB());
        }
    }
    IEnumerator Icon_CB()
    {
        WWW url = new WWW(string.Format("https://graph.facebook.com/{0}/picture?type=small", MyChampionData.FBID));
        Texture2D t = new Texture2D(128, 128);
        yield return url;
        if (url.error == null)
        {
            url.LoadImageIntoTexture(t);
            MySprite = IOManager.ChangeTextureToSprite(t);
            Icon_Sprite.sprite = MySprite;
            IOManager.SaveTextureAsPNG(t, string.Format("{0}/{1}", Application.persistentDataPath, MyChampionData.FBID));
        }
        else
        {
            GetLocalHostIcon();
        }
    }
    void GetLocalHostIcon()
    {
        if (File.Exists(string.Format("{0}/{1}", Application.persistentDataPath, MyChampionData.FBID)))
        {
            MySprite = IOManager.LoadPNGAsSprite(string.Format("{0}/{1}", Application.persistentDataPath, MyChampionData.FBID));
            Icon_Sprite.sprite = MySprite;
        }
    }
}
