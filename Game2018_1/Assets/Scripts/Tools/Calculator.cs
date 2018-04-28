using UnityEngine;
using System.Collections;

public class Calculator
{
    /// <summary>
    /// 傳入ID陣列與權重陣列取得索引
    /// </summary>
    public static int WeightIndexGetter(int[] _ids,int[] _weigths)
    {
        if(_ids.Length!=_weigths.Length)
        {
            Debug.Log("傳入計算器的ID陣列與權重陣列不一致");
            return 0;
        }
        int index = 0;
        int sumWeight=0;
        for (int i = 0; i < _weigths.Length;i++ )
        {
            sumWeight += _weigths[i];
        }
        int randWeight = Random.Range(0, sumWeight);
        for (int i = 0; i < _ids.Length; i++)
        {
            randWeight -= _weigths[i];
            if (randWeight < 0)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
