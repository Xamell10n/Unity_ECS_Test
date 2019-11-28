using Enemies;
using GameManagers;
using LeoECS.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;
using ChangeMovementStateSystem = LeoECS_MultiThreading.Systems.ChangeMovementStateSystem;
using ChangeViewPositionSystem = LeoECS_MultiThreading.Systems.ChangeViewPositionSystem;
using MovementToPositionSystem = LeoECS_MultiThreading.Systems.MovementToPositionSystem;

namespace LeoECS_MultiThreading
{
    public class LeoECSMultiThreadingGameManager : BaseGameManager, IInitializable, ITickable, ILateDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        public LeoECSMultiThreadingGameManager
        (
            EnemyController.LeoECSFactory leoFactory,
            int count,
        
            [Inject(Id = "Game")] EcsWorld world,
            UpdateTimeSystem updateTimeSystem,
            ChangeMovementStateSystem changeMovementStateSystem,
            MovementToPositionSystem movementToPositionSystem,
            ChangeViewPositionSystem changeViewPositionSystem
        ) : base(leoFactory, count)
        {
            _world = world;
            _systems = new EcsSystems(_world)
                .Add(updateTimeSystem)
                .Add(changeMovementStateSystem)
                .Add(movementToPositionSystem)
                .Add(changeViewPositionSystem);
        }

        public void Initialize()
        {
            Debug.Log("Initialize");
//#if UNITY_EDITOR
//        EcsWorldObserver.Create (_world);
//#endif
            _systems.Init();
            for (var i = 0; i < _count; i++)
                _factory.Create();
//#if UNITY_EDITOR
//        EcsSystemsObserver.Create (_systems);
//#endif
        }

        public void Tick()
        {
            _systems.Run();
            _world.EndFrame();
        }

        public void LateDispose()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}