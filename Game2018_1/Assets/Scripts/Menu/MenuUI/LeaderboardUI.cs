using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{

    [SerializeField]
    GameObject LB_Prefab;
    [SerializeField]
    Transform ItemParent;
    [SerializeField]
    Text Leaderboard_Text;
    public static int MaxItemNum = 50;
    static int TotalChampionNum;
    bool IsSpawn;


    static LeaderboardUI Myself;
    static List<ChampionData> CDList;
    static List<LeaderboardItemUI> LBIList;


    void OnEnable()
    {

        Myself = this;
        ServerRequest.GetLeaderboard();
        Leaderboard_Text.text = GameDictionary.String_UIDic["Leaderboard"].GetString(Player.UseLanguage);
    }

    public static void GetChampionData(string _str)
    {
        CDList = new List<ChampionData>();
        string[] dataStr = _str.Split(',');
        TotalChampionNum = int.Parse(dataStr[1]);
        ChampionData.TotalChampionNum = TotalChampionNum;
        string[] chData = dataStr[0].Split('/');
        for (int i = 0; i < chData.Length; i++)
        {
            string[] data = chData[i].Split('$');
            ChampionData cd = new ChampionData(data[0], int.Parse(data[1]), data[2], i + 1);
            CDList.Add(cd);
        }
        if (!Myself.IsSpawn)
            Myself.SpawnItem(CDList);
        else
            Myself.RefreshItems(CDList);
        Player.LeaderBoard_CB(_str);
    }

    public void SpawnItem(List<ChampionData> _list)
    {
        LBIList = new List<LeaderboardItemUI>();
        for (int i = 0; i < MaxItemNum; i++)
        {
            GameObject go = Instantiate(LB_Prefab, Vector3.zero, Quaternion.identity, ItemParent) as GameObject;
            LeaderboardItemUI item = go.GetComponent<LeaderboardItemUI>();
            LBIList.Add(item);
            if(i<_list.Count)
            {
                item.Initialize(_list[i]);
            }
            else
            {
                go.SetActive(false);
            }
        }
        Myself.IsSpawn = true;
    }
    public void RefreshItems(List<ChampionData> _list)
    {
        if (LBIList == null)
            return;
        int dataCount = _list.Count;
        for (int i = 0; i < MaxItemNum; i++)
        {
            if (i < dataCount)
            {
                LBIList[i].gameObject.SetActive(true);
                LBIList[i].Initialize(_list[i]);
            }
            else
            {
                LBIList[i].gameObject.SetActive(false);
            }
        }
    }
}
