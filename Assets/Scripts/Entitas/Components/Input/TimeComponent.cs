using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Enemies.Components.Input
{
    [Unique]
    [Input]
    public class TimeComponent : IComponent
    {
        public float DeltaTime;
        public float RealTimeSinceStarup;
    }
}