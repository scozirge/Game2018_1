using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoPrefab : MonoBehaviour
{
    [SerializeField]
    protected AudioPlayer MyAudio;
    [SerializeField]
    protected AudioClip HitShieldAduio;
    [SerializeField]
    protected AudioClip HitAduio;
    [SerializeField]
    protected AudioClip HitWallAduio;

    public bool IsLaunching { get; protected set; }
    protected Vector3 Force;
    protected Rigidbody2D MyRigi;
    public virtual int BaseDamage { get; protected set; }
    public virtual int Damage { get { return BaseDamage; } }
    public bool IsDavestated = false;
    public virtual void Init(Dictionary<string, object> _dic)
    {
        IsLaunching = false;
        MyRigi = transform.GetComponent<Rigidbody2D>();
        BaseDamage = (int)_dic["Damage"];
        BattleManager.AddToStartPauseFnc(PauseGame);
        BattleManager.AddToEndPauseFnc(ResumeGame);
    }
    protected virtual void Update()
    {
    }
    public virtual void Launch()
    {
        MyRigi.AddForce(Force);
        IsLaunching = true;
    }
    protected virtual void OnTriggerEnter2D(Collider2D _col)
    {
    }
    public virtual void SelfDestroy()
    {
        IsDavestated = true;
        BattleManager.RemoveFromStartPauseFnc(PauseGame);
        BattleManager.RemoveFromEndPauseFnc(ResumeGame);
        BattleManager.CheckAliveAmmoToContinueShoot();
        Destroy(gameObject);
    }


    protected Vector2 SavedVelocity;
    protected float SavedAngularVelocity;

    protected virtual void PauseGame()
    {
        SavedVelocity = MyRigi.velocity;
        SavedAngularVelocity = MyRigi.angularVelocity;
        MyRigi.velocity = Vector2.zero;
        MyRigi.angularVelocity = 0;
    }

    protected virtual void ResumeGame()
    {
        MyRigi.velocity = SavedVelocity;
        MyRigi.angularVelocity = SavedAngularVelocity;
    }
}
