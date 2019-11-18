using Unity.Entities;
using Unity.Jobs;
using UnityEngine.Jobs;

namespace Enemies.Unity_ECS.Systems
{
    public class MovementToPositionSystem : JobComponentSystem
    {
        public struct MovementToPositionParallelForTransformJob : IJobParallelForTransform
        {
            public void Execute(int index, TransformAccess transform)
            {
                
            }
        }

        public struct MovementToPositionParallelForJob : IJobParallelFor
        {
            public void Execute(int index)
            {
                
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return default(JobHandle);
        }
    }
}