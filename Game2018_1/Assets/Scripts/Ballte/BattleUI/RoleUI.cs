using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RoleUI : MonoBehaviour
{

    [SerializeField]
    Image Health;
    [SerializeField]
    Image Shield;

    public virtual void Init()
    {
    }

    public virtual void UpdateHealthUI(float _healthRatio)
    {
        if (_healthRatio < 0.15f)
            _healthRatio = 0.15f;
        Health.fillAmount = _healthRatio;
    }
    public virtual void RotateShield(float _angle)
    {
        Shield.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        Arm();
    }
    public virtual void Disarm()
    {
        Shield.gameObject.SetActive(false);
    }
    public virtual void Arm()
    {
        Shield.gameObject.SetActive(true);
    }
    public virtual void SetPosition(Vector2 _pos)
    {
        transform.localPosition = _pos;
    }
    public virtual void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
