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
    
    [Serializable]
    public class PositionComponent : ComponentDataProxy<PositionData> { }
}