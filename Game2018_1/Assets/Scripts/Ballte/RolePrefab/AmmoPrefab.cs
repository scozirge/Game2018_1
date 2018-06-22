using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoPrefab : MonoBehaviour
{
    public bool IsLaunching { get; protected set; }
    protected Vector3 Force;
    protected Rigidbody2D MyRigi;
    protected Vector3 ShootPos;
    public int Damage { get; protected set; }
    public virtual void Init(Vector3 _shooterPos,int _damage)
    {
        IsLaunching = false;
        MyRigi = transform.GetComponent<Rigidbody2D>();
        ShootPos = _shooterPos;
        Damage = _damage;
    }
    protected virtual void Update()
    {
    }
    public virtual void Launch(Vector2 _force)
    {
        Force = _force;
        MyRigi.AddForce(_force);
        IsLaunching = true;
    }
    public virtual void Launch()
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
    protected virtual void SpawnParticleOnSelf(string _effectName)
    {
        GameObject particlePrefab = Resources.Load(string.Format("Particles/{0}/{0}", _effectName)) as GameObject;
        GameObject particleGo = Instantiate(particlePrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        particleGo.transform.SetParent(transform);
        particleGo.transform.localPosition = Vector3.zero;
    }
    protected virtual void SpawnParticleOnPos(string _effectName)
    {
        GameObject particlePrefab = Resources.Load(string.Format("Particles/{0}/{0}", _effectName)) as GameObject;
        GameObject particleGo = Instantiate(particlePrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        particleGo.transform.position = transform.position;
    }
}
