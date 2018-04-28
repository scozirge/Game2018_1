using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TextManager
{
    /// <summary>
    /// 字串以字元分割轉int[]
    /// </summary>
    public static int[] StringSplitToIntArray(string _str, char _char)
    {
        int[] result;
        string[] resultStr = _str.Split(_char);
        result = new int[resultStr.Length];
        for (int i = 0; i < resultStr.Length; i++)
        {
            result[i] = int.Parse(resultStr[i]);
        }
        return result;
    }
    /// <summary>
    /// 字串以字元分割轉List<int>
    /// </summary>
    public static List<int> StringSplitToIntList(string _str, char _char)
    {
        List<int> result = new List<int>();
        string[] resultStr = _str.Split(_char);
        for (int i = 0; i < resultStr.Length; i++)
        {
            result.Add(int.Parse(resultStr[i]));
        }
        return result;
    }
    /// <summary>
    /// 字串以字元分割轉字串[]
    /// </summary>
    public static string[] StringSplitToStringArray(string _str, char _char)
    {
        string[] result = _str.Split(_char);
        return result;
    }
    /// <summary>
    /// int陣列轉字串並以字元分割
    /// </summary>
    /// <returns></returns>
    public static string IntArrayToStringSplitByChar(int[] _ints, char _char)
    {
        string result = "";
        for (int i = 0; i < _ints.Length; i++)
        {
            if (i != 0)
                result += _char;
            result += _ints[i].ToString();
        }
        return result;
    }
    /// <summary>
    /// 小數轉為百分比
    /// </summary>
    public static int ToPercent(float _value)
    {
        return Mathf.RoundToInt(_value * 100);
    }
}
