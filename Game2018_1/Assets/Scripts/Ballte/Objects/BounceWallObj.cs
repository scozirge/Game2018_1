using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWallObj : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer MySR;
    [SerializeField]
    BoxCollider2D MyCollider;
    float Bounciness;
    float UpDownEtraForce;
    float LeftRightExtraForce;
    public float SlicedHeight { get; private set; }


    public void SetWall(float _bounciness, float _slicedHeight, float _upDownEtraForce, float _leftRightExtraForce)
    {
        Bounciness = _bounciness;
        SlicedHeight = _slicedHeight;
        UpDownEtraForce = _upDownEtraForce;
        LeftRightExtraForce = _leftRightExtraForce;
        MySR.size = new Vector2(MySR.size.x, _slicedHeight);
        MyCollider.size = new Vector2(MySR.size.x, MySR.size.y);
    }

    public Vector2 GetVelocity(Vector2 _velocity)
    {
        _velocity *= Bounciness;
        _velocity.x *= (1 + LeftRightExtraForce);
        _velocity.y *= (1 + UpDownEtraForce);
        return _velocity;
    }
}
