using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoSpawner : MonoBehaviour
{

    [SerializeField]
    EnemyAmmo ThatAmmoPrefab;
    List<EnemyAmmo> AmmoList;



    public void ShootAmmo()
    {
        for (int i = 0; i < AmmoList.Count; i++)
        {
            AmmoList[i].Shoot();
        }
    }

    public void SpawnAmmo(Vector3 _shooterPos)
    {
        int ammoNum = 4;
        float radius = 150f;
        AmmoList = new List<EnemyAmmo>();
        for (int i = 0; i < ammoNum; i++)
        {
            GameObject ammoGo = Instantiate(ThatAmmoPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            EnemyAmmo ea = ammoGo.GetComponent<EnemyAmmo>();
            float divAngle = 360 / ammoNum;
            float x = radius * Mathf.Cos(i * divAngle * Mathf.Deg2Rad) + _shooterPos.x;
            float y = radius * Mathf.Sin(i * divAngle * Mathf.Deg2Rad) + _shooterPos.y;
            ammoGo.transform.SetParent(transform);
            ammoGo.transform.position = new Vector2(x, y);
            ea.Init(_shooterPos);
            ea.SetCircularMotion(radius, i * divAngle * Mathf.Deg2Rad);
            AmmoList.Add(ea);
        }
    }
}
