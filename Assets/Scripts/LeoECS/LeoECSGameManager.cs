using Enemies;
using GameManagers;
using LeoECS.Systems;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using Zenject;

public class LeoECSGameManager : BaseGameManager, IInitializable, ITickable, ILateDisposable
{
    private readonly EcsWorld _world;
    private readonly EcsSystems _systems;

    public LeoECSGameManager
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
