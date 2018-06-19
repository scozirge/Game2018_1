using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInBattle
{

    public static bool CanCtrl { get; private set; }
    [SerializeField]
    Camera CtrlCamera;//Camera
    Touch Touch;//curent Touch
    Vector2 TouchWorldPoint;//touchPoint's world point
    int CurTounchCount;//current touch  object number
    Collider2D[] Go_CurTouchTargets;//current touch targets

    void InitBattleCtrl()
    {
        CanCtrl = true;
    }
    public void TouchDetect()//觸碰偵測
    {
        if (!CanCtrl)
            return;
        CurTounchCount = 0;
        while (CurTounchCount < Input.touchCount)
        {
            Touch = Input.GetTouch(CurTounchCount);
            switch (Touch.phase)
            {
                case TouchPhase.Moved://拖曳執行
                    DragTouch();
                    break;
                case TouchPhase.Began://開始觸碰時執行
                    BeganTouch();
                    break;
                case TouchPhase.Stationary://常按執行
                    //DragTouch();
                    break;
                case TouchPhase.Ended://放開時執行

                    EndTouch();
                    break;
            }
            ++CurTounchCount;
        }
    }
    void BeganTouch()//觸碰
    {
        Debug.Log("a");
        //判斷觸碰點
        TouchWorldPoint = CtrlCamera.ScreenToWorldPoint(Input.GetTouch(CurTounchCount).position);
        if (Physics2D.OverlapPoint(TouchWorldPoint) != null)
        {
            Debug.Log("b");
            Go_CurTouchTargets = Physics2D.OverlapPointAll(TouchWorldPoint);
            for (int i = 0; i < Go_CurTouchTargets.Length; i++)
            {
                Debug.Log("c");
                if (Go_CurTouchTargets[i].tag == "Monster")
                {
                    Debug.Log("d");
                    MonsterPrefab target = Go_CurTouchTargets[i].GetComponent<MonsterPrefab>();
                    SetTarget(target);
                }
            }
        }
    }

    void DragTouch()//拖曳
    {
    }
    void EndTouch()
    {
    }
}
