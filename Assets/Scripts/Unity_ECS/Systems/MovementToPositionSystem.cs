using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Enemies.Unity_ECS.Systems
{
    public class MovementToPositionSystem : JobComponentSystem
    {
        [BurstCompile]
        struct MovementToPositionJob : IJobForEach<MovementData, PositionData>
        {
            public float DeltaTime;
            
            public void Execute(ref MovementData c0, ref PositionData c1)
            {
                c1.Value += c0.Direction.normalized * c0.Speed * DeltaTime;
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var movementToPositionJob = new MovementToPositionJob
            {
                DeltaTime = Time.deltaTime
            };
            var movementToPositionJobHandle = movementToPositionJob.Schedule(this);
            return movementToPositionJobHandle;
        }
    }
}