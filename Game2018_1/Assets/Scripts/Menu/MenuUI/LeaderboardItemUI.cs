using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderboardItemUI : MonoBehaviour
{
    [SerializeField]
    Text Name_Text;
    [SerializeField]
    Text Score_Text;
    [SerializeField]
    Image Icon;

    ChampionData MyChampionData;

    public void Initialize(ChampionData _data)
    {
        MyChampionData = _data;
        Name_Text.text = MyChampionData.Name;
        Score_Text.text = MyChampionData.Score.ToString();
    }
}
