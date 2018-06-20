using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrefab : MonoBehaviour
{
    delegate void DelegateAction1(string _str, float _flo);
    static DelegateAction1 CameraAction;
    delegate void DelegateAction2(string _str);
    static DelegateAction2 CameraEffect;
    [SerializeField]
    Animator MyAni;
    [SerializeField]
    GameObject BloodPrefab;

    void Start()
    {
        CameraAction += PlayMotion;
        CameraEffect += PlayEffect;
    }
    public static void DoAction(string _str, float _flo)
    {
        if (CameraAction != null)
            CameraAction(_str, _flo);
    }
    public static void DoEffect(string _str)
    {
        if (CameraEffect != null)
            CameraEffect(_str);
    }
    void PlayEffect(string _str)
    {
        switch (_str)
        {
            case "Blood":
                Blood();
                break;
            default:
                break;
        }
    }
    void PlayMotion(string _motion, float _normalizedTime)
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
    void Blood()
    {
        GameObject bloodGo = Instantiate(BloodPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        bloodGo.transform.position = Vector3.zero;
    }
}
