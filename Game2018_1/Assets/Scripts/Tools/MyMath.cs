using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath : MonoBehaviour
{

    public static float GetAngerFormTowPoint2D(Vector2 _form, Vector2 _to)
    {
        Vector2 vector = _form - _to;
        float angle = (float)((Mathf.Atan2(vector.x, vector.y) / Mathf.PI) * 180f);
        if (angle < 0) angle += 360f;
        return angle;
    }
    public static int GetNumber1DividedByNumber2(float _number1, float _number2)
    {
        return (int)(Mathf.Round(_number1 / _number2));
    }
    public static float Calculate_ReturnFloat(float _num1, float _num2, Operator _operator)
    {
        float result = 0;
        switch (_operator)
        {
            case Operator.Plus:
                result = _num1 + _num2;
                break;
            case Operator.Minus:
                result = _num1 - _num2;
                break;
            case Operator.Times:
                result = _num1 * _num2;
                break;
            case Operator.Divided:
                if (_num2==0)
                {
                    result = 0;
                    Debug.LogWarning("不可除以0");
                }
                else
                    result = _num1 / _num2;
                break;
            case Operator.Equal:
                result = Mathf.Round(_num2);
                break;
        }
        return result;
    }
    public static int Calculate_ReturnINT(float _num1, float _num2, Operator _operator)
    {
        int result = 0;
        switch (_operator)
        {
            case Operator.Plus:
                result = (int)Mathf.Round(_num1 + _num2);
                break;
            case Operator.Minus:
                result = (int)Mathf.Round(_num1 - _num2);
                break;
            case Operator.Times:
                result = (int)Mathf.Round(_num1 * _num2);
                break;
            case Operator.Divided:
                result = (int)Mathf.Round(_num1 / _num2);
                break;
            case Operator.Equal:
                result = (int)(Mathf.Round(_num2));
                break;
        }
        return result;
    }
}
