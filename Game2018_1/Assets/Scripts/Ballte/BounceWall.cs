using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class BattleManager : MonoBehaviour
{
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
    float Bounciness;
    [SerializeField]
    float UpDownEtraForce;
    [SerializeField]
    float LeftRightExtraForce;


    public static void SetBounceWall()
    {
        MySelf.MyBounceWall.gameObject.SetActive(true);

        float rand = Random.Range(0, 2);
        float dir = (rand == 0) ? dir = 1 : -1;
        MySelf.MyBounceWall.SetWall(MySelf.Bounciness, Random.Range(MySelf.MinWallLength, MySelf.MaxWallLength), MySelf.UpDownEtraForce, MySelf.LeftRightExtraForce);
        MySelf.MyBounceWall.transform.position = new Vector2(MySelf.PosX * dir, Random.Range(MySelf.MinYPos, MySelf.MaxYPos));
    }
    public static void HideBounceWall()
    {
        MySelf.MyBounceWall.gameObject.SetActive(false);
    }
}
