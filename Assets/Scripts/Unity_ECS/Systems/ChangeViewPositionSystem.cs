using Unity.Entities;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace Enemies.Unity_ECS.Systems
{
    public class ChangeViewPositionSystem : JobComponentSystem
    {
        public struct ChangeViewPositionJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess transform)
            {
                
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return default(JobHandle);
        }
    }
}