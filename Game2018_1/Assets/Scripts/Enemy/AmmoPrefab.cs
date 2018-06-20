using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPrefab : MonoBehaviour
{
    float LifeTime;
    Vector3 Force;
    Rigidbody2D MyRigi;
    float AngularVlocity = 3f;
    float Radius { get; set; }
    float StartRadian;
    float CurRadian;
    Vector3 Center;
    public bool IsLaunching { get; protected set; }

    public void Init(Vector3 _shooterPos, float _radius, float _startAngle)
    {
        LifeTime = 2;
        IsLaunching = false;
        MyRigi = transform.GetComponent<Rigidbody2D>();
        Radius = _radius;
        Center = _shooterPos;
        StartRadian = _startAngle;
        CurRadian = StartRadian;

    }
    void Update()
    {
        CircularMotion();
        LifeTimeCountDown();
    }
    void OnTriggerEnter2D(Collider2D _col)
    {
        switch (_col.gameObject.tag)
        {
            case "Player":
                Destroy(gameObject);
                CameraPrefab.DoEffect("Blood");
                break;
            default:
                break;
        }
    }
    void LifeTimeCountDown()
    {
        if (!IsLaunching)
            return;
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            Destroy(gameObject);
    }
    void CircularMotion()
    {
        if (IsLaunching)
            return;
        CurRadian += AngularVlocity * Time.deltaTime;
        float x = Radius * Mathf.Cos(CurRadian) + Center.x;
        float y = Radius * Mathf.Sin(CurRadian) + Center.y;
        transform.position = new Vector2(x, y);
    }
    public void Shoot()
    {
        IsLaunching = true;
        Force = (transform.position - Center).normalized * 30000;
        MyRigi.AddForce(Force);
    }
}
