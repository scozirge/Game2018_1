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
    public Sprite MySprite { get; private set; }

    ChampionData MyChampionData;

    public void Initialize(ChampionData _data)
    {
        MyChampionData = _data;
        Name_Text.text = MyChampionData.Name;
        Score_Text.text = MyChampionData.Score.ToString();
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
