using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterPrefab : MonoBehaviour
{
    delegate void OneDelegate();
    static OneDelegate MonsterUnarm;
    [SerializeField]
    Transform Trans_Shield;
    public int ShieldAngle { get; protected set; }
    bool BeginUnarm;
    float UnArmTimer;
    

    void Start()
    {
        MonsterUnarm += BeginToUnarm;
        BeginUnarm = false;
    }
    void Update()
    {
        CountDownToUnArm();
    }
    public void BeStruck()
    {
        PlayMotion("BeStruck", 0);
    }
    void CountDownToUnArm()
    {
        if (!BeginUnarm)
            return;
        UnArmTimer -= Time.deltaTime;
        if (UnArmTimer <= 0)
        {
            UnArm();
        }
    }
    public static void SetAllMonsterUnarm()
    {
        if (MonsterUnarm != null)
            MonsterUnarm();
    }
    void Move()
    {
        float x = Random.Range(-2, 2);
        transform.position = new Vector2(x, 4);
    }
    public void Arm()
    {
        Move();
        Trans_Shield.gameObject.SetActive(true);
        ShieldAngle = Random.Range(0, 360);
        Trans_Shield.rotation = Quaternion.Euler(new Vector3(0, 0, ShieldAngle));
    }
    void UnArm()
    {
        Trans_Shield.gameObject.SetActive(false);
        BeginUnarm = false;
        BallSpawner.SetCanShoot(true);
    }
    public void BeginToUnarm()
    {
        BeginUnarm = true;
        UnArmTimer = 0.5f;
    }


}
