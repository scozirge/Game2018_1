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
    float TailDrawMaxDistance;
    [SerializeField]
    float BowDrawMaxAngle;
    [SerializeField]
    Animator MyAni;
    [SerializeField]
    Image AimLine;


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
        AimLine.gameObject.SetActive(false);
    }

    public void BowDraw(float _angle,float _proportion)
    {
        //front sight rotate
        Aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        //draw a bow
        TailDrawDistance = _proportion * TailDrawMaxDistance;
        Tail.transform.localPosition = new Vector2(TailOriginalPos.x, TailOriginalPos.y - TailDrawDistance);

        BowDrawAngle = _proportion * BowDrawMaxAngle;
        RightBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Right - BowDrawAngle));
        LeftBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Left + BowDrawAngle));

        //draw a aimLine
        AimLine.gameObject.SetActive(true);
        AimLine.rectTransform.localScale = new Vector2(AimLine.rectTransform.localScale.x, 1 + (1f * _proportion));
        AimLine.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
    }
    public void Release()
    {
        PlayMotion("ReleaseBow", 0);
        RightBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Right));
        LeftBow.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, InitBowRotation_Left));
        AimLine.gameObject.SetActive(false);
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
