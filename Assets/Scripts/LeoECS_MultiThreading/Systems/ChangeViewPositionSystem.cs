using LeoECS.Components;
using Leopotam.Ecs;
using Leopotam.Ecs.Threads;

namespace LeoECS_MultiThreading.Systems
{
    public class ChangeViewPositionSystem : EcsMultiThreadSystem<EcsFilter<PositionComponent, ViewComponent>>
    {
        private EcsFilter<PositionComponent, ViewComponent> _positionToViewFilter = null;

        protected override EcsFilter<PositionComponent, ViewComponent> GetFilter()
        {
            return _positionToViewFilter;
        }

        protected override EcsMultiThreadWorker GetWorker()
        {
            return Worker;
        }

        protected override int GetMinJobSize()
        {
            return 11000;
        }

        protected override int GetThreadsCount()
        {
            return System.Environment.ProcessorCount - 1;
        }
        
        private static void Worker(EcsMultiThreadWorkerDesc workerDesc)
        {
            foreach (var i in workerDesc)
            {
                var filter = workerDesc.Filter;
                var position = filter.Get1[i];
                var view = filter.Get2[i];
                view.Transform.position = position.Value;
            }
        }

    }
}