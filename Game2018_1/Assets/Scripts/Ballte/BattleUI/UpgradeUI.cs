using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    Text ChoseSkill_Title;
    [SerializeField]
    SkillBoardPrefab MySkillPrefab;
    [SerializeField]
    Transform Parent_Trans;
    [SerializeField]
    int SpawnSkillNum;

    List<SkillBoardPrefab> SkillBoardList;

    void OnEnable()
    {
        ChoseSkill_Title.text = GameDictionary.String_UIDic["ChoseSkill"].GetString(Player.UseLanguage);
    }

    public void SetSkillBoard()
    {
        if (SkillBoardList == null || SkillBoardList.Count <= 0)
            SpawnNewSkillBoard();
        else
            RefreshSkillBoard();
    }
    public void SpawnNewSkillBoard()
    {
        if (SpawnSkillNum <= 0) return;
        List<SkillData> skillDatas = SkillData.GetRandomSkills(SpawnSkillNum,
    BattleManager.MyPlayerRole.ShieldLevel > 0 ? false : true);
        SkillBoardList = new List<SkillBoardPrefab>();
        //Spawn
        for (int i = 0; i < SpawnSkillNum; i++)
        {

            GameObject skillBoardGo = Instantiate(MySkillPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            SkillBoardPrefab sbp = skillBoardGo.GetComponent<SkillBoardPrefab>();
            sbp.Init(skillDatas[i]);
            skillBoardGo.transform.SetParent(Parent_Trans);
            SkillBoardList.Add(sbp);
        }
    }
    public void RefreshSkillBoard()
    {
        if (SpawnSkillNum <= 0) return;
        List<SkillData> skillDatas = SkillData.GetRandomSkills(SkillBoardList.Count,
            BattleManager.MyPlayerRole.ShieldLevel > 0 ? false : true);
        //Spawn
        for (int i = 0; i < skillDatas.Count; i++)
        {
            SkillBoardList[i].Init(skillDatas[i]);
        }
    }

}
