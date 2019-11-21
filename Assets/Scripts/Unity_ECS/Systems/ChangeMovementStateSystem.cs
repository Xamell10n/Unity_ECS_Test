using System.Threading;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Scripting;

namespace Enemies.Unity_ECS.Systems
{
    public class ChangeMovementStateSystem : JobComponentSystem
    {
        private readonly BaseSceneContext.MovementData _data;

        [Preserve]
        public ChangeMovementStateSystem
        (
            BaseSceneContext.MovementData data
        )
        {
            _data = data;
        }
        
        [BurstCompile]
        private struct ChangeMovementJob : IJobForEach<MovementStateData, MovementData>
        {
            public float TimeSinceStarup;
            public float WaitTime;
            public float MoveTime;
            public float Speed;
            public Vector3 Direction;
            
            public void Execute(ref MovementStateData c0, ref MovementData c1)
            {
                if (c0.FinishMovementStateTime <= TimeSinceStarup)
                {
                    float delta;
                    if (c1.IsMoving())
                    {
                        c1.Speed = 0;
                        delta = WaitTime;
                    }
                    else
                    {
                        c1.Speed = Speed;
                        c1.Direction = Direction;
                        delta = MoveTime;
                    }
                    c0.FinishMovementStateTime = TimeSinceStarup + delta;
                }
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var changeMovementJob = new ChangeMovementJob
            {
                Direction = Helpers.GetRandomAndNormalizeVector3(),
                MoveTime = _data.MovementTimeRange.GetRandom(),
                Speed = _data.SpeedRange.GetRandom(),
                TimeSinceStarup = Time.realtimeSinceStartup,
                WaitTime = _data.WaitTimeRange.GetRandom()
            };
            var chagneMovementJobHandle = changeMovementJob.Schedule(this);
            return chagneMovementJobHandle;
        }
    }
}