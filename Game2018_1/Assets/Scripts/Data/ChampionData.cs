using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionData
{
    LeaderboardItemUI MyUI;
    public delegate void CB();
    public string Name { get; private set; }
    public int Score { get; private set; }
    public string FBID { get; private set; }

    public ChampionData(string _name, int _score, string _fbID)
    {
        Name = _name;
        Score = _score;
        FBID = _fbID;
    }
}
