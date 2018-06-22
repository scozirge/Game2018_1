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
}
