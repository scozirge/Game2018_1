using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterPrefab
{
    [SerializeField]
    Animator MyAni;

    public void PlayMotion(string _motion, float _normalizedTime)
    {
        switch (_motion)
        {
            case "Default":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyAni.Play(_motion, 0, _normalizedTime);
                break;
            case "BeStruck":
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
