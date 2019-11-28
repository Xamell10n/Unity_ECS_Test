using LeoECS.Components;
using Leopotam.Ecs;
using Leopotam.Ecs.Threads;

namespace LeoECS_MultiThreading.Systems
{
    public class MovementToPositionSystem : EcsMultiThreadSystem<EcsFilter<MovementComponent, PositionComponent>>
    {
        private EcsFilter<MovementComponent, PositionComponent> _movementToPositionFilter = null;
        private EcsFilter<TimeComponent> _timeFilter = null;
        
        protected override EcsFilter<MovementComponent, PositionComponent> GetFilter()
        {
            return _movementToPositionFilter;
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
            var deltaTime = _timeFilter.Get1[0].DeltaTime;
            var filter = workerDesc.Filter;
            foreach (var i in filter)
            {
                var movement = filter.Get1[i];
                var position = filter.Get2[i];
                var delta = deltaTime * movement.Direction * movement.Speed;
                position.Value += delta;
            }
        }

    }
}