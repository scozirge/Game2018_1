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
    [SerializeField]
    Animator MyAni;


    float TailDrawDistance;
    float BowDrawAngle;
    Vector2 TailOriginalPos;
    float InitBowRotation_Left;
    float InitBowRotation_Right;

    public override void Init()
    {
        base.Init();
        TailOriginalPos = Tail.transform.localPosition;
        InitBowRotation_Left = LeftBow.rectTransform.localRotation.eulerAngles.z;
        InitBowRotation_Right = RightBow.rectTransform.localRotation.eulerAngles.z;
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
        RightBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Right - BowDrawAngle));
        LeftBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Left + BowDrawAngle));

    }
    public void Release()
    {
        PlayMotion("ReleaseBow", 0);
        RightBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Right));
        LeftBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Left));
    }
    void PlayMotion(string _motion, float _normalizedTime)
    {
        switch (_motion)
        {
            case "Default":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyAni.Play(_motion, 0, _normalizedTime);
                break;
            case "ReleaseBow":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyAni.Play(_motion, 0, _normalizedTime);
                else
                    MyAni.StopPlayback();//重播
                break;
            default:
                break;
        }
    }

}
