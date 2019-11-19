using System.Collections.Generic;
using Enemies;
using Enemies.Base;
using Enemies.OneUpdate;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Zenject;

namespace GameManagers
{
    public class OneUpdateWithJobGameManager : BaseGameManager, IInitializable, ITickable, ILateDisposable
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly List<OneUpdateWithJobMovementController> _controllersAll;
        
        private TransformAccessArray _transforms;
        private NativeList<float> _speeds;
        private NativeList<Vector3> _directions;
        private JobHandle _moveHandle;
        
        public OneUpdateWithJobGameManager
        (
            EnemyController.Factory factory,
            BaseSceneContext.MovementData data,
            int count
        ) : base(factory, count)
        {
            _data = data;
            
            _controllersAll = new List<OneUpdateWithJobMovementController>();
            
            _directions = new NativeList<Vector3>(Allocator.TempJob);
            _speeds = new NativeList<float>(Allocator.TempJob);
            _transforms = new TransformAccessArray(0, -1);
        }
        
        [BurstCompile]
        public struct MoveJobTransfrom : IJobParallelForTransform
        {
            public float Delta;
            [ReadOnly] public NativeList<Vector3> Directions;
            [ReadOnly] public NativeList<float> Speeds;
            
            public void Execute(int index, TransformAccess transform)
            {
                transform.position += Directions[index].normalized * Delta * Speeds[index];
            }
        }
        
        public struct MoveJobNative : IJobParallelFor
        {
            private NativeArray<Vector3> _positions;
            private readonly NativeArray<Vector3> _directions;
            private readonly NativeArray<float> _speeds;
            private readonly float _delta;

            public MoveJobNative
            (
                NativeArray<Vector3> positions,
                NativeArray<Vector3> directions,
                NativeArray<float> speeds,
                float delta
            )
            {
                _positions = positions;
                _directions = directions;
                _speeds = speeds;
                _delta = delta;
            }
            
            public void Execute(int index)
            {
                _positions[index] += _directions[index] * _speeds[index] * _delta;
            }
        }

        public struct MoveJob : IJobParallelFor
        {
            public float Delta;
            public Transform[] Transforms;
            public OneUpdateWithJobMovementController[] MovementControllers;

            public void Execute(int index)
            {
                Transforms[index].position += MovementControllers[index].Direction.normalized * MovementControllers[index].Speed * Delta;
            }
        }
        
        public void Initialize()
        {
            Debug.Log("Initialize");
            for (var i = 0; i < _count; i++)
            {
                var controller = _factory.Create().GetComponent<OneUpdateWithJobMovementController>();
                _controllersAll.Add(controller);
                
                _transforms.Add(controller.transform);
                _speeds.Add(controller.Speed);
                _directions.Add(controller.Direction);
            }
        }

        public void Tick()
        {
            _moveHandle.Complete();
            var deltaTime = Time.deltaTime;
            var timeSinceStartup = Time.realtimeSinceStartup;
            for (var i = 0; i < _controllersAll.Count; i++)
            {
                var controller = _controllersAll[i];
                if (controller.FinishMovenetStateTime <= timeSinceStartup)
                {
                    float delta;
                    float speed;
                    if (controller.IsMoving)
                    {
                        speed = 0f;
                        delta = _data.WaitTimeRange.GetRandom();
                    }
                    else
                    {
                        speed = _data.SpeedRange.GetRandom();
                        controller.Direction = Helpers.GetRandomAndNormalizeVector3();
                        delta = _data.MovementTimeRange.GetRandom();
                    }
                    controller.FinishMovenetStateTime = timeSinceStartup + delta;
                    controller.Speed = speed;
                }

                _directions[i] = controller.Direction;
                _speeds[i] = controller.Speed;
            }

            var job = new MoveJobTransfrom
            {
                Delta = deltaTime,
                Directions = _directions,
                Speeds = _speeds
            };
            _moveHandle = job.Schedule(_transforms);
            JobHandle.ScheduleBatchedJobs();
        }

        public void LateDispose()
        {
            _directions.Dispose();
            _speeds.Dispose();
            _transforms.Dispose();
        }
    }
}