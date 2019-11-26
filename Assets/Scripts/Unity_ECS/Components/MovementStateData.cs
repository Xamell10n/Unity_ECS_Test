using System;
using Unity.Entities;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct MovementStateData : IComponentData
    {
        public float FinishMovementStateTime;
    }
}