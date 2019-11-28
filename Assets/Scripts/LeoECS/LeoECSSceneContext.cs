using System.Collections;
using System.Collections.Generic;
using Enemies;
using GameManagers;
using LeoECS.Systems;
using Leopotam.Ecs;
using UnityEngine;
[CreateAssetMenu(menuName = "Installers/LeoEcsData", fileName = "LeoECSData")]
public class LeoECSSceneContext : BaseSceneContext
{
    public override void InstallBindings()
    {
        BindSpawnData<EnemyController.LeoECSFactory>();
        Container.BindInterfacesAndSelfTo<LeoECSGameManager>().AsSingle().NonLazy();
        Container.Bind<MovementData>().FromInstance(_movementData);
        
        BindWorlds();
        BindSystems();
    }

    private void BindSystems()
    {
        Container.Bind<UpdateTimeSystem>().AsSingle();
        Container.Bind<ChangeMovementStateSystem>().AsSingle();
        Container.Bind<ChangeViewPositionSystem>().AsSingle();
        Container.Bind<MovementToPositionSystem>().AsSingle();
    }

    private void BindWorlds()
    {
        Container.BindInstance(new EcsWorld()).WithId("Game").AsSingle();
    }
}
