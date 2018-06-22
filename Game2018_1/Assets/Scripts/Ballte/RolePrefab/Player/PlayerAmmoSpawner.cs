using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoSpawner : MonoBehaviour
{
    [SerializeField]
    Transform Trans_SpawnPos;
    [SerializeField]
    PlayerAmmo PlayerAmmoPrefab;


    public void Spawn(Vector3 _shooterPos, Vector2 _force, int _damage)
    {
        GameObject ballGo = Instantiate(PlayerAmmoPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        PlayerAmmo pa = ballGo.GetComponent<PlayerAmmo>();
        pa.transform.SetParent(transform);
        pa.transform.localPosition = Trans_SpawnPos.localPosition;
        pa.Init(_shooterPos, _damage);
        pa.Launch(_force);
    }
}
