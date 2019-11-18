using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Enemies.Unity_ECS.Systems
{
    public class UpdateTimeSineStartupSystem : JobComponentSystem
    {
        struct TimeSinceStartupJob : IJobForEach<TimeSinceStartupData>
        {
            public void Execute(ref TimeSinceStartupData c0)
            {
                c0.Value = Time.realtimeSinceStartup;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new TimeSinceStartupJob();
            var timeSinceStartupHandler = job.Schedule(this);
            return timeSinceStartupHandler;
        }
    }
}