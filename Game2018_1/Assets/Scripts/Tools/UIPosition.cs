using UnityEngine;
using System.Collections;

public class UIPosition
{
    public static void UIToWorldPos(RectTransform _canvasRect, Camera _camera, Transform _targetTrans, RectTransform _selfTrans,float _diffPosX,float _diffPosY)
    {
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
        Vector2 ViewportPosition = _camera.WorldToViewportPoint(_targetTrans.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * _canvasRect.sizeDelta.x) - (_canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * _canvasRect.sizeDelta.y) - (_canvasRect.sizeDelta.y * 0.5f)));
        Vector2 diffPos = new Vector2(_diffPosX, _diffPosY);
        //now you can set the position of the ui element
        _selfTrans.anchoredPosition = WorldObject_ScreenPosition + diffPos;
    }
}
