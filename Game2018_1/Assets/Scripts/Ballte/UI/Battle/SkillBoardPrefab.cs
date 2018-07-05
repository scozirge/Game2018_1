using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBoardPrefab : MonoBehaviour
{

    [SerializeField]
    Text Name_Text;
    [SerializeField]
    Text Name_Description;
    [SerializeField]
    Image Icon;

    SkillData RelyData;

    public void Init(SkillData _data)
    {
        RelyData = _data;
        Name_Text.text = RelyData.Name;
        Name_Description.text = RelyData.Description;
        Icon.sprite = _data.GetSkillICON();
    }
    public void Choice()
    {
        BattleManager.MyPlayerRole.GetUpgrade(RelyData);
        BattleCanvas.EndCallSkillBoard();
        BattleManager.NextLevel();
    }
}
