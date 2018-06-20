using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{

    [SerializeField]
    AmmoPrefab ThatAmmoPrefab;
    List<AmmoPrefab> AmmoList;

    public int CurHealth { get; protected set; }
    public int MaxHealth { get; protected set; }


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
        float radius = 1.5f;
        AmmoList = new List<AmmoPrefab>();
        for (int i = 0; i < ammoNum; i++)
        {
            GameObject ammoGo = Instantiate(ThatAmmoPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
            AmmoPrefab ap = ammoGo.GetComponent<AmmoPrefab>();
            float divAngle = 360 / ammoNum;
            float x = radius * Mathf.Cos(i * divAngle * Mathf.Deg2Rad) + _shooterPos.x;
            float y = radius * Mathf.Sin(i * divAngle * Mathf.Deg2Rad) + _shooterPos.y;
            ammoGo.transform.SetParent(transform);
            ammoGo.transform.position = new Vector2(x, y);
            ap.Init(_shooterPos, radius, i * divAngle * Mathf.Deg2Rad);
            AmmoList.Add(ap);
        }
    }
}
