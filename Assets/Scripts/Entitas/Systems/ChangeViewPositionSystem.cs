using System.Collections.Generic;
using Entitas;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Unity_Entitas.Systems
{
    public class ChangeViewPositionSystem : ReactiveSystem<GameEntity>
    {
        private NativeArray<Vector3> _positionArray;
        private TransformAccessArray _transformAccessArray;
        private MoveJob _moveJob;
        private JobHandle _jobHandle;

        public ChangeViewPositionSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.PositionVector);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasPositionVector && entity.hasView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var length = entities.Count;
            _transformAccessArray = new TransformAccessArray(length);
            _positionArray = new NativeArray<Vector3>(length, Allocator.TempJob);
            for (var i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                _transformAccessArray.Add(entity.view.GameObject.transform);
                _positionArray[i] = entity.positionVector.Value;
            }

            _moveJob = new MoveJob
            {
                Positions = _positionArray
            };
            _jobHandle = _moveJob.Schedule(_transformAccessArray);
            _jobHandle.Complete();

            _transformAccessArray.Dispose();
            _positionArray.Dispose();
        }
    
        [BurstCompile]
        private struct MoveJob : IJobParallelForTransform
        {
            [ReadOnly] public NativeArray<Vector3> Positions;

            public void Execute(int index, TransformAccess transform)
            {
                transform.position = Positions[index];
            }
        }
    }}
