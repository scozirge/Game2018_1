using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    float LifeTime;
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        LifeTime = ps.main.duration + ps.main.startLifetimeMultiplier + ps.main.startDelayMultiplier;
    }
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            Destroy(gameObject);
    }
}
