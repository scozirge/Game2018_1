using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWallObj : WallObj
{
    [SerializeField]
    public Direction Dir;
    float MaxXDragForce;
    float MaxYDragForce;

    public void SetWall(float _maxDragForce, float _maxYDragForce)
    {
        MaxXDragForce = _maxDragForce;
        MaxYDragForce = _maxYDragForce;
    }
    public override Vector2 GetVelocity(Vector2 _velocity)
    {
        Vector2 v = base.GetVelocity(_velocity);
        switch (Dir)
        {
            case Direction.Top:
                return new Vector2(v.x, v.y * -1);
            case Direction.Bottom:
                return new Vector2(v.x, v.y * -1);
            case Direction.Left:
                return new Vector2(v.x * -1, v.y);
            case Direction.Right:
                return new Vector2(v.x * -1, v.y);
            default:
                return new Vector2(v.x, v.y);
        }
    }
    public Vector2 GetDragForce(Vector2 _velocity, float _dragProportion)
    {
        return new Vector2(_velocity.x * (1 + MaxXDragForce * _dragProportion), _velocity.y * (1 + MaxYDragForce * _dragProportion));
    }
}
