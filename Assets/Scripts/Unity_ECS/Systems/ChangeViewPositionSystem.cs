using Unity.Entities;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace Enemies.Unity_ECS.Systems
{
    public class ChangeViewPositionSystem : JobComponentSystem
    {
        public struct ChangeViewPositionJob : IJobForEach<PositionData, ViewData>
        {
            public void Execute(ref PositionData c0, ref ViewData c1)
            {
                c1.View.transform.position = c0.Value;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return default(JobHandle);
        }
    }
}