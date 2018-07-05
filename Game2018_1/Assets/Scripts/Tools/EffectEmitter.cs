using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEmitter : MonoBehaviour
{

    public static void EmitParticle(string _effectName, Vector3 _pos, Vector3 _dir, Transform _parent)
    {
        GameObject particlePrefab = Resources.Load<ParticleSystem>(string.Format("Particles/{0}/{0}", _effectName)).gameObject;
        if (particlePrefab == null)
        {
            Debug.LogWarning("No particle prefab are assigned:" + string.Format("Particles/{0}/{0}", _effectName));
            return;
        }
        GameObject particleGo = Instantiate(particlePrefab.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        if (_parent)
            particleGo.transform.SetParent(_parent);

        particleGo.transform.localPosition = _pos;

        particleGo.transform.rotation = Quaternion.Euler(_dir);
    }

}
