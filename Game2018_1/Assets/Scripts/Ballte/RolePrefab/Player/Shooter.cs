using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerRole
{
    [SerializeField]
    PlayerAmmoSpawner MyAmmoSpawner;
    [SerializeField]
    GameObject StartPosPrefab;
    [SerializeField]
    GameObject EndPosPrefab;
    [SerializeField]
    int MaxSpeed;
    [SerializeField]
    int MinSpeed;
    [SerializeField]
    int DragForce;

    bool IsPress;
    public static bool CanShoot { get; protected set; }
    Vector3 StartPos;
    Vector3 CurPos;
    Vector3 EndPos;
    GameObject Go_StartPos;
    GameObject Go_EndPos;
    protected void InitShooter()
    {
        SetCanShoot(true);
        Go_EndPos = Instantiate(StartPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos = Instantiate(EndPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos.SetActive(false);
        Go_EndPos.SetActive(false);
    }

    void ClickToSpawn()
    {
        if (!CanShoot)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            StartPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);

            Go_StartPos.SetActive(true);
            Go_EndPos.SetActive(true);
            Go_EndPos.transform.position = StartPos;
            Go_StartPos.transform.position = StartPos;
            Target.Arm();
            IsPress = true;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            CurPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
            Go_EndPos.transform.position = CurPos;;
            float angle = MyMath.GetAngerFormTowPoint2D(CurPos, StartPos);
            BattleCanvas.PlayerBowDraw(180 - angle, Vector2.Distance(CurPos, StartPos));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (IsPress)
                IsPress = false;
            else
                return;
            Go_StartPos.SetActive(false);
            Go_EndPos.SetActive(false);
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            EndPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
            MyAmmoSpawner.Spawn(transform.position, GetForce(), Attack);
            SetCanShoot(false);
            Target.LaunchAmmo();
        }
    }
    public static void SetCanShoot(bool _canShoot)
    {
        CanShoot = _canShoot;
    }
    Vector3 GetForce()
    {
        float speed = Vector3.Distance(StartPos, EndPos) * DragForce;
        if (speed < MinSpeed)
            speed = MinSpeed;
        else if (speed > MaxSpeed)
            speed = MaxSpeed;
        Vector3 dir = (StartPos - EndPos).normalized;
        if (dir == Vector3.zero)
            dir = Vector3.up;
        return dir * speed;
    }
}
