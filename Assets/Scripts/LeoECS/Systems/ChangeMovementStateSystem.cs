using LeoECS.Components;
using Leopotam.Ecs;
using Zenject;

namespace LeoECS.Systems
{
    public class ChangeMovementStateSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly BaseSceneContext.MovementData _data;

        private EcsFilter<MovementStateComponent> _movementStateFilter = null;
        private EcsFilter<TimeComponent> _timeFilter = null;

        public ChangeMovementStateSystem
        (
            BaseSceneContext.MovementData data
        )
        {
            _data = data;
        }
    
        public void Run()
        {
            var realTimeSinceStartup = _timeFilter.Get1[0].RealTimeSinceStartup;
            for (var i = 0; i < _movementStateFilter.GetEntitiesCount(); i++)
            {
                var movementState = _movementStateFilter.Get1[i];
                if (movementState.FinishMovementStateTime <= realTimeSinceStartup)
                {
                    float delta;
                    ref var entity = ref _movementStateFilter.Entities[i];
                    var movement = entity.Get<MovementComponent>(); 
                    if (movement != null)
                    {
                        entity.Unset<MovementComponent>();
                        delta = _data.WaitTimeRange.GetRandom();
                    }
                    else
                    {
                        var speed = _data.SpeedRange.GetRandom();
                        var direction = Helpers.GetRandomAndNormalizeVector3();
                        movement = entity.Set<MovementComponent>();
                        movement.Direction = direction;
                        movement.Speed = speed;
                        delta = _data.MovementTimeRange.GetRandom();
                    }
                    movementState.FinishMovementStateTime = realTimeSinceStartup + delta;
                }

            }
        }

        public void Init()
        {
            
        }
    }
}