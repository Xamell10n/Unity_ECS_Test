using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Enemies.Components.Input
{
    [Input]
    [Unique]
    public class TimeSinceStartupComponent : IComponent
    {
        public float Value;
    }
}