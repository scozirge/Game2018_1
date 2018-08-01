using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public partial class PlayerRole
{
    [SerializeField]
    public PlayerAmmoSpawner MyAmmoSpawner;
    [SerializeField]
    GameObject StartPosPrefab;
    [SerializeField]
    GameObject EndPosPrefab;
    [SerializeField]
    int MaxSpeed;
    [SerializeField]
    int MinSpeed;
    [SerializeField]
    int MaxDragDistance;


    bool IsPress;
    public static bool CanShoot { get; protected set; }
    Vector3 StartPos;
    Vector3 CurPos;
    Vector3 EndPos;
    GameObject Go_StartPos;
    GameObject Go_EndPos;
    float DragProportion;

    protected void InitShooter()
    {
        SetCanShoot(true);
        Go_EndPos = Instantiate(StartPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos = Instantiate(EndPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos.SetActive(false);
        Go_EndPos.SetActive(false);
    }
    static GraphicRaycaster m_Raycaster;
    static EventSystem m_EventSystem;
    static PointerEventData m_PointerEventData;
    public static void SetGraphicRaycaster(GraphicRaycaster _gr, EventSystem _es)
    {
        m_Raycaster = _gr;
        m_EventSystem = _es;
    }

    void ClickToSpawn()
    {
        if (BattleManager.IsPause)
            return;
        if (!CanShoot)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;
            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "UI")
                {
                    return;
                }
            }

            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            StartPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);

            Go_StartPos.SetActive(true);
            Go_EndPos.SetActive(true);
            Go_EndPos.transform.position = StartPos;
            Go_StartPos.transform.position = StartPos;
            Target.Arm();
            BattleManager.SetBounceWall();
            IsPress = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (!IsPress)
                return;
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            CurPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
            Go_EndPos.transform.position = CurPos; ;
            float angle = MyMath.GetAngerFormTowPoint2D(CurPos, StartPos);
            BattleCanvas.PlayerBowDraw(180 - angle, GetDragProportion());
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
            CurPos = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
            EndPos = CurPos;
            Shoot();
        }
    }
    void Shoot()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add("Damage", Attack);
        data.Add("ShootPos", transform.position);
        data.Add("Force", GetForce());
        data.Add("AmmoBounceTimes", AmmoBounceTimes);
        data.Add("AmmoBounceDamage", AmmoBounceDamage);
        data.Add("DragProportion", GetDragProportion());
        MyAmmoSpawner.Spawn(data);
        Target.LaunchAmmo();
        SetCanShoot(false);
        BattleCanvas.PlayerReleaseBow();
    }
    public static void SetCanShoot(bool _canShoot)
    {
        CanShoot = _canShoot;
    }
    Vector3 GetForce()
    {
        int extraSpeed = MaxSpeed - MinSpeed;
        Vector3 dir = (StartPos - EndPos).normalized;
        if (dir == Vector3.zero)
            dir = Vector3.up;
        return dir * (MinSpeed + (extraSpeed * GetDragProportion()));
    }
    float GetDragProportion()
    {
        float dragProportion = 0;
        float dragDistance = Vector3.Distance(StartPos, CurPos);
        if (dragDistance > MaxDragDistance)
            dragProportion = 1;
        else
            dragProportion = dragDistance / MaxDragDistance;
        return dragProportion;
    }
}
