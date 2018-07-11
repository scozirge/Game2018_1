using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoSpawner : MonoBehaviour
{
    [SerializeField]
    Transform Trans_SpawnPos;
    [SerializeField]
    PlayerAmmo PlayerAmmoPrefab;

    public List<PlayerAmmo> MyAmmos;


    public void Spawn(Dictionary<string, object> _data)
    {
        MyAmmos = new List<PlayerAmmo>();
        GameObject ballGo = Instantiate(PlayerAmmoPrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        PlayerAmmo pa = ballGo.GetComponent<PlayerAmmo>();
        MyAmmos.Add(pa);
        pa.transform.SetParent(transform);
        pa.transform.localPosition = Trans_SpawnPos.localPosition;
        pa.Init(_data);
        pa.Launch();
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
}
