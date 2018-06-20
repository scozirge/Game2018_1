using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrefab : MonoBehaviour
{

    Rigidbody2D myRigibody;
    [SerializeField]
    GameObject BloodPrefab;
    [SerializeField]
    GameObject BloodPrefab2;

    public int MaxBounceTimes { get; protected set; }
    public int curBounceTimes { get; protected set; }

    public void Init(Vector2 _force)
    {
        myRigibody = gameObject.GetComponent<Rigidbody2D>();
        MaxBounceTimes = 3;
        curBounceTimes = 0;
        Launch(_force);
    }
    void Launch(Vector2 _force)
    {
        myRigibody.AddForce(_force);
        SpawnBlood2();
        BallSpawner.SetCanShoot(false);
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        Rotate();
        switch (_col.gameObject.tag)
        {
            case "HCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    myRigibody.velocity = new Vector2(myRigibody.velocity.x * -1, myRigibody.velocity.y);
                break;
            case "VCollider":
                CameraPrefab.DoAction("Shake", 0);
                if (Bounce())
                    myRigibody.velocity = new Vector2(myRigibody.velocity.x, myRigibody.velocity.y * -1);
                break;
            case "EnemyShield":
                SelfDestroy();
                break;
            case "Monster":
                _col.GetComponent<MonsterPrefab>().BeStruck();
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
        myRigibody.AddTorque(300);
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
    void SelfDestroy()
    {
        SpawnBlood();
        Destroy(gameObject);
        MonsterPrefab.SetAllMonsterUnarm();
    }
}
