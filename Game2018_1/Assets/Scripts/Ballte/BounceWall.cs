using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class BattleManager : MonoBehaviour
{
    [SerializeField]
    List<NormalWallObj> MyNormalWall;
    [SerializeField]
    BounceWallObj MyBounceWall;
    [SerializeField]
    float MaxWallLength;
    [SerializeField]
    float MinWallLength;
    [SerializeField]
    float MaxYPos;
    [SerializeField]
    float MinYPos;
    [SerializeField]
    float PosX;
    [SerializeField]
    float RedWallBounciness;
    [SerializeField]
    float RedWallUpDownEtraForce;
    [SerializeField]
    float RedWallLeftRightExtraForce;
    [SerializeField]
    float MaxXDragForce;
    [SerializeField]
    float MaxYDragForce;


    void SetNormanWall()
    {
        for (int i = 0; i < MyNormalWall.Count; i++)
        {
            MyNormalWall[i].SetWall(MaxXDragForce, MaxYDragForce);
        }
    }

    public static void SetBounceWall()
    {
        MySelf.MyBounceWall.gameObject.SetActive(true);

        float rand = Random.Range(0, 2);
        float dir = (rand == 0) ? dir = 1 : -1;
        MySelf.MyBounceWall.SetWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinWallLength, MySelf.MaxWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
        MySelf.MyBounceWall.transform.position = new Vector2(MySelf.PosX * dir, Random.Range(MySelf.MinYPos, MySelf.MaxYPos));
    }
    public static void HideBounceWall()
    {
        MySelf.MyBounceWall.gameObject.SetActive(false);
    }
}
