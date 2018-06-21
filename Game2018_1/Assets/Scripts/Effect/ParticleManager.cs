using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public bool Loop;
    public float LifeTime;

    private ParticleSystem ps;
    void Start()
    {
        if (LifeTime > 0 || Loop)
            return;
        ps = GetComponent<ParticleSystem>();
        LifeTime = ps.main.duration + ps.main.startLifetimeMultiplier + ps.main.startDelayMultiplier;
    }
    void LifeTimeCountDown()
    {
        if (Loop)
            return;
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
            Destroy(gameObject);
    }
    void Update()
    {
        LifeTimeCountDown();
    }
}
