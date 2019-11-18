using System;
using Unity.Entities;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    [Serializable]
    public struct PositionData : IComponentData
    {
        public Vector3 Value;
    }
    
    public class PositionComponent : ComponentDataProxy<PositionData> { }
}