using System;
using Unity.Entities;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct MovementData : IComponentData
    {
        public float Speed;
        public Vector3 Direction;
    }
    
    public class MovementComponent : ComponentDataProxy<MovementData> { }
}