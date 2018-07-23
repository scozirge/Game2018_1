using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWallObj : WallObj
{
    [SerializeField]
    SpriteRenderer MySR;
    [SerializeField]
    BoxCollider2D MyCollider;
    [SerializeField]
    float Bounciness;
    public float SlicedHeight { get; private set; }
    public float UpDownEtraForce { get; protected set; }
    public float LeftRightExtraForce { get; protected set; }

    public void SetWall(float _bounciness, float _slicedHeight, float _upDownEtraForce, float _leftRightExtraForce)
    {
        Bounciness = _bounciness;
        SlicedHeight = _slicedHeight;
        UpDownEtraForce = _upDownEtraForce;
        LeftRightExtraForce = _leftRightExtraForce;
        MySR.size = new Vector2(MySR.size.x, _slicedHeight);
        MyCollider.size = new Vector2(MySR.size.x, MySR.size.y);
    }
    public override Vector2 GetVelocity(Vector2 _velocity)
    {
        Vector2 v = base.GetVelocity(_velocity);
        v *= Bounciness;
        v.x *= (1 + LeftRightExtraForce);
        v.y *= (1 + UpDownEtraForce);
        return v;
    }

}
