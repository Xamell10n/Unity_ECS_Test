using LeoECS.Components;
using Leopotam.Ecs;

namespace LeoECS.Systems
{
    public class MovementToPositionSystem : IEcsRunSystem
    {
        private EcsFilter<MovementComponent, PositionComponent> _movementToPositionFilter = null;
        private EcsFilter<TimeComponent> _timeFilter = null;
        
        public void Run()
        {
            var deltaTime = _timeFilter.Get1[0].DeltaTime;
            foreach (var i in _movementToPositionFilter)
            {
                var movement = _movementToPositionFilter.Get1[i];
                var position = _movementToPositionFilter.Get2[i];
                var delta = deltaTime * movement.Direction * movement.Speed;
                position.Value += delta;
            }
        }
    }
}