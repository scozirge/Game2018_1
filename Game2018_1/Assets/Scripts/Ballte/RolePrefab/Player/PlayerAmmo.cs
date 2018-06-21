using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : AmmoPrefab
{

    [SerializeField]
    GameObject BloodPrefab;
    [SerializeField]
    GameObject BloodPrefab2;

    public int MaxBounceTimes { get; protected set; }
    public int curBounceTimes { get; protected set; }

    public void Init(Vector2 _force)
    {
        MyRigi = gameObject.GetComponent<Rigidbody2D>();
        MaxBounceTimes = 3;
        curBounceTimes = 0;
        Launch(_force);
    }
    void Launch(Vector2 _force)
    {
        MyRigi.AddForce(_force);
        SpawnBlood2();
        PlayerAmmoSpawner.SetCanShoot(false);
    }
    protected override void OnTriggerEnter2D(Collider2D _col)
    {
        base.OnTriggerEnter2D(_col);
        Rotate();
        switch (_col.gameObject.tag)
        {
            case "HCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x * -1, MyRigi.velocity.y);
                break;
            case "VCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    MyRigi.velocity = new Vector2(MyRigi.velocity.x, MyRigi.velocity.y * -1);
                break;
            case "EnemyShield":
                SelfDestroy();
                break;
            case "Monster":
                _col.GetComponent<EnemyRole>().BeStruck();
                SelfDestroy();
                break;
            default:
                break;
        }
    }
    bool Bounce()
    {
        SpawnBlood();
        curBounceTimes++;
        if (curBounceTimes > MaxBounceTimes)
        {
            SelfDestroy();
            return false;
        }
        return true;
    }
    void Rotate()
    {
        MyRigi.AddTorque(300);
    }
    void SpawnBlood()
    {
        GameObject bloodGo = Instantiate(BloodPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        bloodGo.transform.position = transform.position;
    }
    void SpawnBlood2()
    {
        GameObject bloodGo = Instantiate(BloodPrefab2.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        bloodGo.transform.SetParent(transform);
        bloodGo.transform.position = transform.position;
    }
    protected override void SelfDestroy()
    {
        SpawnBlood();
        EnemyRole.SetAllMonsterUnarm();
        base.SelfDestroy();
    }
}
