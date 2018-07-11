using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionData
{

    public string Name { get; private set; }
    public int Score { get; private set; }

    public ChampionData(string _name,int _score)
    {
        Name = _name;
        Score = _score;
    }
}
