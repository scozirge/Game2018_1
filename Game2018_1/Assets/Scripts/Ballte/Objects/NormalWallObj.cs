using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWallObj : WallObj
{
    [SerializeField]
    public Direction Dir;
    float MaxDragForce;

    public void SetWall(float _maxDragForce)
    {
        MaxDragForce = _maxDragForce;
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
        Debug.Log(new Vector2(_velocity.x * (1 + MaxDragForce * _dragProportion), _velocity.y));
        return new Vector2(_velocity.x * (1 + MaxDragForce * _dragProportion), _velocity.y);
    }
}
