using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseUI : MonoBehaviour
{

    [SerializeField]
    Animator MyPhaseAni;
    public void PlayMotion(string _motion, float _normalizedTime)
    {
        switch (_motion)
        {
            case "Win":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyPhaseAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyPhaseAni.Play(_motion, 0, _normalizedTime);
                else
                    MyPhaseAni.StopPlayback();//重播
                break;
            case "NextLevel":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyPhaseAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyPhaseAni.Play(_motion, 0, _normalizedTime);
                else
                    MyPhaseAni.StopPlayback();//重播
                break;
            default:
                break;
        }
    }
    public void WinEnd()
    {
        BattleManager.NextLevel();
    }
    public void NextLevelEnd()
    {
        BattleManager.ReStartGame();
    }
}
