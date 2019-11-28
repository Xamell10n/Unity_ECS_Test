using Entitas;
using UnityEngine;

namespace Enemies.Components
{
    [Game]
    public class PositionVectorComponent : IComponent
    {
        public Vector3 Value;
    }
}