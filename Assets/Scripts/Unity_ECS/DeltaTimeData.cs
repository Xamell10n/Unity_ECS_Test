using System;
using Unity.Entities;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct DeltaTimeData : IComponentData
    {
        public float Value;
    }
}