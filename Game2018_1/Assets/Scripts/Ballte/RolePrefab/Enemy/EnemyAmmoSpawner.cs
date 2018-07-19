using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoSpawner : MonoBehaviour
{
    [SerializeField]
    EnemyAmmo ThatAmmoPrefab;
    List<EnemyAmmo> MyAmmos;



    public void LaunchAmmo()
    {
        if (MyAmmos == null)
            return;
        for (int i = 0; i < MyAmmos.Count; i++)
        {
            MyAmmos[i].Launch();
        }
    }
    public bool CheckAlifeAmmo()
    {
        if (MyAmmos == null)
            return true;
        for (int i = 0; i < MyAmmos.Count; i++)
        {
            if (!MyAmmos[i].IsDavestated)
                return true;
        }
        return false;
    }
    public void DestroyAllAmmos()
    {
        if (MyAmmos == null)
            return;
        for (int i = 0; i < MyAmmos.Count; i++)
        {
            if (MyAmmos[i] != null)
                MyAmmos[i].SelfDestroy();
        }
    }
    public void SpawnAmmo(Dictionary<string, object> _data)
    {
        float radius = 150f;
        MyAmmos = new List<EnemyAmmo>();
        int ammoNum = (int)_data["AmmoNum"];
        Vector3 shooterPos = (Vector3)_data["ShooterPos"];

        for (int i = 0; i < ammoNum; i++)
        {
            GameObject ammoGo = Instantiate(ThatAmmoPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            ammoGo.name = string.Format("enemyAmmo{0}", i);
            EnemyAmmo ea = ammoGo.GetComponent<EnemyAmmo>();
            float divAngle = 360 / ammoNum;
            float x = radius * Mathf.Cos(i * divAngle * Mathf.Deg2Rad) + shooterPos.x;
            float y = radius * Mathf.Sin(i * divAngle * Mathf.Deg2Rad) + shooterPos.y;
            ammoGo.transform.SetParent(transform);
            ammoGo.transform.position = new Vector2(x, y);
            ea.Init(_data);
            ea.SetCircularMotion(radius, i * divAngle * Mathf.Deg2Rad);
            MyAmmos.Add(ea);
        }
    }
}
