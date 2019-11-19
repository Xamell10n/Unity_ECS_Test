using Entitas;
using UnityEngine;

namespace Enemies.Components
{
    [Game]
    public class MovementComponent : IComponent
    {
        public Vector3 Direction;
        public float Speed;
    }
}