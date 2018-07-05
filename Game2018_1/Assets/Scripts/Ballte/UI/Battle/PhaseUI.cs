using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    [SerializeField]
    Text Round_Title;
    [SerializeField]
    Text Round_Level;
    [SerializeField]
    Animator MyPhaseAni;

    void SetText()
    {
        Round_Title.text = GameDictionary.String_UIDic["Round"].GetString(Player.UseLanguage);
        Round_Level.text = BattleManager.Level.ToString();
    }

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
            case "Lose":
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyPhaseAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyPhaseAni.Play(_motion, 0, _normalizedTime);
                else
                    MyPhaseAni.StopPlayback();//重播
                break;
            case "StartGame":
                SetText();
                if (Animator.StringToHash(string.Format("Base Layer.{0}", _motion)) != MyPhaseAni.GetCurrentAnimatorStateInfo(0).fullPathHash)
                    MyPhaseAni.Play(_motion, 0, _normalizedTime);
                else
                    MyPhaseAni.StopPlayback();//重播
                break;
            case "NextLevel":
                SetText();
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
        BattleManager.Upgrade();
    }
    public void LoseEnd()
    {
        BattleManager.Settlement();
    }
    public void NextLevelEnd()
    {
        BattleManager.NextGame();
    }
    public void StartGameEnd()
    {
        BattleManager.SetPause(false);
    }
}
