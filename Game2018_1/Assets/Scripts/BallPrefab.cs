using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPrefab : MonoBehaviour
{

    Rigidbody2D myRigibody;
    [SerializeField]
    GameObject BloodPrefab;

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
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        switch (_col.gameObject.tag)
        {
            case "HCollider":
                if (Bounce())
                    myRigibody.velocity = new Vector2(myRigibody.velocity.x * -1, myRigibody.velocity.y);

                break;
            case "VCollider":
                if (Bounce())
                    myRigibody.velocity = new Vector2(myRigibody.velocity.x, myRigibody.velocity.y * -1);
                break;
            default:
                break;
        }
    }
    bool Bounce()
    {
        curBounceTimes++;
        if (curBounceTimes > MaxBounceTimes)
        {
            Destroy(gameObject);
            return false;
        }
        GameObject bloodGo = Instantiate(BloodPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        bloodGo.transform.position = transform.position;
        return true;
    }
}
