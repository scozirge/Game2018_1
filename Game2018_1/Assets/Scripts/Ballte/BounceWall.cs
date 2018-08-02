using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class BattleManager : MonoBehaviour
{
    [SerializeField]
    List<NormalWallObj> MyNormalWall;
    [SerializeField]
    List<BounceWallObj> MyBounceWall;
    [SerializeField]
    float MaxVerticalWallLength;
    [SerializeField]
    float MinVerticalWallLength;
    [SerializeField]
    float MaxHorizontalWallLength;
    [SerializeField]
    float MinHorizontalWallLength;
    [SerializeField]
    Vector2 VerticalWallYPosRange;
    [SerializeField]
    int VerticalWallXpos;
    [SerializeField]
    Vector2 HorizontalWallXPosRange;
    [SerializeField]
    int VerticalWallYpos;
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
        int rndSide = Random.Range(0, 3);
        switch (rndSide)
        {
            case 0:
                MySelf.MyBounceWall[0].SetVerticalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinVerticalWallLength, MySelf.MaxVerticalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[0].transform.position = new Vector2(MySelf.VerticalWallXpos, Random.Range(MySelf.VerticalWallYPosRange.x, MySelf.VerticalWallYPosRange.y));
                MySelf.MyBounceWall[1].SetHorizontalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinHorizontalWallLength, MySelf.MaxHorizontalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[1].transform.position = new Vector2(Random.Range(MySelf.HorizontalWallXPosRange.x, MySelf.HorizontalWallXPosRange.y), MySelf.VerticalWallYpos);
                break;
            case 1:
                MySelf.MyBounceWall[0].SetVerticalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinVerticalWallLength, MySelf.MaxVerticalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[0].transform.position = new Vector2(MySelf.VerticalWallXpos - 1, Random.Range(MySelf.VerticalWallYPosRange.x, MySelf.VerticalWallYPosRange.y));
                MySelf.MyBounceWall[1].SetHorizontalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinHorizontalWallLength, MySelf.MaxHorizontalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[1].transform.position = new Vector2(Random.Range(MySelf.HorizontalWallXPosRange.x * -1, MySelf.HorizontalWallXPosRange.y), MySelf.VerticalWallYpos);
                break;
            case 2:
                MySelf.MyBounceWall[0].SetVerticalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinVerticalWallLength, MySelf.MaxVerticalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[0].transform.position = new Vector2(MySelf.VerticalWallXpos, Random.Range(MySelf.VerticalWallYPosRange.x, MySelf.VerticalWallYPosRange.y));
                MySelf.MyBounceWall[1].SetVerticalWall(MySelf.RedWallBounciness, Random.Range(MySelf.MinVerticalWallLength, MySelf.MaxVerticalWallLength), MySelf.RedWallUpDownEtraForce, MySelf.RedWallLeftRightExtraForce);
                MySelf.MyBounceWall[1].transform.position = new Vector2(MySelf.VerticalWallXpos * -1, Random.Range(MySelf.VerticalWallYPosRange.x * -1, MySelf.VerticalWallYPosRange.y));
                break;
        }
        for (int i = 0; i < MySelf.MyBounceWall.Count; i++)
        {
            MySelf.MyBounceWall[i].gameObject.SetActive(true);
        }



    }
    public static void HideBounceWall()
    {
        for (int i = 0; i < MySelf.MyBounceWall.Count; i++)
        {
            MySelf.MyBounceWall[i].gameObject.SetActive(false);
        }
    }
}
