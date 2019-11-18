using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Enemies.Unity_ECS.Systems
{
    public class UpdataDeltaTimeSystem : JobComponentSystem
    {
        struct DeltaTimeJob : IJobForEach<DeltaTimeData>
        {
            public void Execute(ref DeltaTimeData c0)
            {
                c0.Value = Time.deltaTime;
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new DeltaTimeJob();
            var deltaTimeHandle = job.Schedule(this);
            return deltaTimeHandle;
        }
    }
}