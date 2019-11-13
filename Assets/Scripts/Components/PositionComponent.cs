using Entitas;
using UnityEngine;

[Game]
public class PositionComponent : IComponent
{
    public float X;
    public float Y;
    public float Z;

    public Vector3 GetVector3()
    {
        var result = new Vector3 (X, Y, Z);
        return result;
    }
}