using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallObj : MonoBehaviour
{


    public virtual Vector2 GetVelocity(Vector2 _velocity)
    {
        return _velocity;
    }
}
