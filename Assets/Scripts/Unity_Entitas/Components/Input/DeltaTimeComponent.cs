using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Enemies.Components.Input
{
    [Input]
    [Unique]
    public class DeltaTimeComponent : IComponent
    {
        public float Value;
    }
}