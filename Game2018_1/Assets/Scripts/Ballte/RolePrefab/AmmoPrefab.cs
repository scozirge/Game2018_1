using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoPrefab : MonoBehaviour
{
    public bool IsLaunching { get; protected set; }
    protected Vector3 Force;
    protected Rigidbody2D MyRigi;
    protected Vector3 ShootPos;
    public virtual void Init(Vector3 _shooterPos)
    {
        IsLaunching = false;
        MyRigi = transform.GetComponent<Rigidbody2D>();
        ShootPos = _shooterPos;
    }
    protected virtual void Update()
    {
    }
    public virtual void Shoot()
    {
        IsLaunching = true;
    }
    protected virtual void OnTriggerEnter2D(Collider2D _col)
    {
    }
    protected virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }
    protected virtual void SpawnEffectOnSelf(string _effectName)
    {
        GameObject particlePrefab = Resources.Load(string.Format("Particles/{0}", "glass")) as GameObject;
        GameObject particleGo = Instantiate(particlePrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        particleGo.transform.position = transform.position;
    }
}
