using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoleUI : RoleUI
{

    [SerializeField]
    Image Aim;
    [SerializeField]
    Image Tail;
    [SerializeField]
    Image LeftBow;
    [SerializeField]
    Image RightBow;
    [SerializeField]
    int TailMaxDrawDistance;
    [SerializeField]
    float BowMaxRotateAngle;
    [SerializeField]
    float TailDrawFactor;
    [SerializeField]
    float BowDrawFactor;


    float TailDrawDistance;
    float BowDrawAngle;
    Vector2 TailOriginalPos;


    public override void Init()
    {
        base.Init();
        TailOriginalPos = Tail.transform.localPosition;
    }

    public void BowDraw(float _angle, float _force)
    {
        //front sight rotate
        Aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        //draw a bow
        TailDrawDistance = _force * TailDrawFactor;
        if (TailDrawDistance > TailMaxDrawDistance)
            TailDrawDistance = TailMaxDrawDistance;
        Tail.transform.localPosition = new Vector2(TailOriginalPos.x, TailOriginalPos.y - TailDrawDistance);

        BowDrawAngle = _force * BowDrawFactor;
        if (BowDrawAngle > BowMaxRotateAngle)
            BowDrawAngle = BowMaxRotateAngle;
        RightBow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -BowDrawAngle));
        LeftBow.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, BowDrawAngle));

    }

}
