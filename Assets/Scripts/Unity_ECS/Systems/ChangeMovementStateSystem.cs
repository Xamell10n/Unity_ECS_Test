using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

namespace Enemies.Unity_ECS.Systems
{
    public class ChangeMovementStateSystem : JobComponentSystem
    {
        private readonly EntityManager _entityManager;

        public ChangeMovementStateSystem()
        {
            
        }
        
        private struct ChangeMovementJob : IJobForEach<MovementStateData, MovementData>
        {
            public EntityManager EntityManager;
            
            public void Execute(ref MovementStateData c0, ref MovementData c1)
            {
                
            }
        }
        
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            
            return default(JobHandle);
        }
    }
}