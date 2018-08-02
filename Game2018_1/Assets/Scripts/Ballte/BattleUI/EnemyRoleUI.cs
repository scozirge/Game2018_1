using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRoleUI : RoleUI
{
    [SerializeField]
    GameObject MyScoreObj;
    [SerializeField]
    Text ScoreTitle_Text;
    [SerializeField]
    Text ScoreValue_Text;
    [SerializeField]
    Animator ScoreAni;

    public void ShowScore(string _str,int _score)
    {
        if (!BattleManager.MyEnemyRole.IsAlive)
            return;
        ScoreTitle_Text.text = _str;
        ScoreValue_Text.text = _score.ToString();
        PlayMotion("Play", 0);
    }
    public void PlayMotion(string _motion, float _normalizedTime)
    {
        switch (_motion)
        {
            case "Default":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != ScoreAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    ScoreAni.Play(_motion, 0, _normalizedTime);
                break;
            case "Play":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != ScoreAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    ScoreAni.Play(_motion, 0, _normalizedTime);
                else
                    ScoreAni.StopPlayback();//重播
                break;
            default:
                break;
        }
    }

}
