using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrefab : MonoBehaviour
{
    static CameraPrefab MyCameraPrefab;
    [SerializeField]
    Animator MyAni;
    [SerializeField]
    GameObject BloodPrefab;

    void Start()
    {
        MyCameraPrefab = this.GetComponent<CameraPrefab>();
    }
    public static void DoAction(string _str, float _normalizedTime)
    {
        MyCameraPrefab.PlayMotion(_str, _normalizedTime);
    }
    public static void DoEffect(string _str)
    {
        MyCameraPrefab.PlayEffect(_str);
    }
    public void PlayEffect(string _str)
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
    void Blood()
    {
        GameObject bloodGo = Instantiate(BloodPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        bloodGo.transform.position = Vector3.zero;
    }
}
