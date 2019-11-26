using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct MovementData : IComponentData
    {
        public float Speed;
        public Vector3 Direction;

        public bool IsMoving()
        {
            var isMoving = Speed > 0;
            return isMoving;
        }
    }
}