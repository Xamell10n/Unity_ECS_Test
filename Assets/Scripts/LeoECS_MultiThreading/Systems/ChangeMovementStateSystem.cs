using LeoECS.Components;
using Leopotam.Ecs;
using Leopotam.Ecs.Threads;

namespace LeoECS_MultiThreading.Systems
{
    public class ChangeMovementStateSystem : EcsMultiThreadSystem<EcsFilter<MovementStateComponent>>
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

        protected override EcsFilter<MovementStateComponent> GetFilter()
        {
            return _movementStateFilter;
        }

        protected override EcsMultiThreadWorker GetWorker()
        {
            return Worker;
        }

        protected override int GetMinJobSize()
        {
            return 1100;
        }

        protected override int GetThreadsCount()
        {
            return System.Environment.ProcessorCount - 1;
        }

        private void Worker(EcsMultiThreadWorkerDesc workerDesc)
        {
            var realTimeSinceStartup = _timeFilter.Get1[0].RealTimeSinceStartup;
            var filter = workerDesc.Filter;
            foreach (var i in filter)
            {
                var movementState = filter.Get1[i];
                if (movementState.FinishMovementStateTime <= realTimeSinceStartup)
                {
                    float delta;
                    ref var entity = ref filter.Entities[i];
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
    }
}