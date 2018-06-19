using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    Vector3 BotLeftSpawnPos;
    [SerializeField]
    MonsterPrefab MyMonsterPrefab;
    [SerializeField]
    int MaxColumn;
    [SerializeField]
    int MaxRow;
    List<MonsterPrefab> MPList = new List<MonsterPrefab>();
    List<int[]> PosList;
    public void SpawnMonster(int _monsterNum, int _lv)
    {
        if (_lv < 0)
        {
            Debug.LogWarning("the monster number or level is Less than or equal to 0");
            _lv = 1;
        }
        if (_monsterNum < 0 || _monsterNum > (MaxColumn * MaxRow))
        {
            Debug.LogWarning("too many monster or monster num is less than or equal to 0");
            _monsterNum = 1;
        }
        Dictionary<string, string> monsterDic = new Dictionary<string, string>();
        monsterDic.Add("Level", _lv.ToString());
        PosList = new List<int[]>();
        int[] pos;
        for (int i = 0; i < MaxColumn; i++)
        {
            for (int j = 0; j < MaxRow; j++)
            {
                pos = new int[2];
                pos[0] = i;
                pos[1] = j;
                PosList.Add(pos);
            }
        }
        for (int i = 0; i < _monsterNum; i++)
        {
            GameObject monsterGo = Instantiate(MyMonsterPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            MonsterPrefab mp = monsterGo.GetComponent<MonsterPrefab>();
            Monster monster = new Monster(monsterDic);
            monsterGo.transform.SetParent(transform);
            int rndNum = Random.Range(0, PosList.Count);
            mp.Init(i, monster, PosList[rndNum]);
            PosList.RemoveAt(rndNum);
            MPList.Add(mp);
        }
    }
}
