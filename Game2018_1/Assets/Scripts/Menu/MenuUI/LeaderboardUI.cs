using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{

    [SerializeField]
    GameObject LB_Prefab;
    [SerializeField]
    Transform ItemParent;
    [SerializeField]
    int MaxItemNum = 100;
    bool IsSpawn;


    static LeaderboardUI Myself;
    static List<ChampionData> CDList;
    static List<LeaderboardItemUI> LBIList;


    void OnEnable()
    {

        Myself = this;
        ServerRequest.GetLeaderboard();            
    }

    public static void GetChampionData(string _str)
    {
        CDList = new List<ChampionData>();
        string[] strData = _str.Split('/');
        for (int i = 0; i < strData.Length; i++)
        {
            string[] data = strData[i].Split('$');
            ChampionData cd = new ChampionData(data[0], int.Parse(data[1]));
            CDList.Add(cd);
        }
        if (!Myself.IsSpawn)
            Myself.SpawnItem(CDList);
        else
            Myself.RefreshItems(CDList);
    }

    public void SpawnItem(List<ChampionData> _list)
    {
        Debug.Log("a");
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
        Debug.Log("b");
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
