using System;
using Unity.Entities;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct TimeSinceStartupData : IComponentData
    {
        public float Value;
    }
}