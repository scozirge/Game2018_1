using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    MonsterPrefab MyMonster;
    [SerializeField]
    AmmoSpawner MyAmmoSpawner;
    [SerializeField]
    Transform Trans_SpawnPos;
    [SerializeField]
    BallPrefab MyBallPrefab;
    [SerializeField]
    Camera MyCamera;
    [SerializeField]
    GameObject StartPosPrefab;
    [SerializeField]
    GameObject EndPosPrefab;

    bool IsPress;
    public static bool CanShoot { get; protected set; }
    Vector3 StartPos;
    Vector3 EndPos;
    GameObject Go_StartPos;
    GameObject Go_EndPos;
    public void Start()
    {
        SetCanShoot(true);
        Go_EndPos = Instantiate(StartPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos = Instantiate(EndPosPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        Go_StartPos.SetActive(false);
        Go_EndPos.SetActive(false);
    }
    public void Update()
    {
        ClickToSpawn();
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
            MyMonster.Arm();
            MyAmmoSpawner.SpawnAmmo(MyMonster.transform.position);
            IsPress = true;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = MyCamera.ScreenPointToRay(Input.mousePosition);
            Go_EndPos.transform.position = ray.origin + (ray.direction * MyCamera.transform.position.z * -1);
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
            Spawn(GetForce());
            MyAmmoSpawner.ShootAmmo();
        }
    }
    Vector3 GetForce()
    {
        float speed = Vector3.Distance(StartPos, EndPos) * 200;
        Debug.Log(speed);
        if (speed < 30000)
            speed = 30000;
        else if (speed > 200000)
            speed = 200000;
        Debug.Log(speed);
        Vector3 dir = (StartPos - EndPos).normalized;
        if (dir == Vector3.zero)
            dir = new Vector3(0, 1, 0);
        return dir * speed;
    }
    public void Spawn(Vector2 _force)
    {
        GameObject ballGo = Instantiate(MyBallPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        BallPrefab bp = ballGo.GetComponent<BallPrefab>();
        bp.transform.SetParent(transform);
        bp.transform.localPosition = Trans_SpawnPos.localPosition;
        bp.Init(_force);
    }
    public static void SetCanShoot(bool _canShoot)
    {
        CanShoot = _canShoot;
    }
}
