using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbabilityGetter
{
    public static bool GetResult(float _probability)
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < Mathf.RoundToInt(_probability * 100))
            return true;
        else
            return false;
    }
    public static int GetFromWeigth(List<int> _weigthList)
    {
        int allWeigth = 0;
        for (int i = 0; i < _weigthList.Count; i++)
        {
            allWeigth += _weigthList[i];
        }
        int randNum = Random.Range(0, allWeigth);
        for (int i = 0; i < _weigthList.Count; i++)
        {
            allWeigth -= _weigthList[i];
            if (allWeigth <= randNum)
            {
                return i;
            }
        }
        Debug.LogWarning("權重取得器錯誤");
        return _weigthList.Count - 1;
    }
}
