using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrefab : MonoBehaviour
{
    delegate void OneDelegate();
    static OneDelegate CameraShake;
    [SerializeField]
    Animator MyAni;

    void Start()
    {
        CameraShake += Shake;
    }
    void Shake()
    {
        PlayMotion("Shake", 0);
    }
    public static void AllCameraShake()
    {
        if (CameraShake != null)
            CameraShake();
    }

    public void PlayMotion(string _motion, float _normalizedTime)
    {
        switch (_motion)
        {
            case "Default":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyAni.Play(_motion, 0, _normalizedTime);
                break;
            case "Shake":
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
