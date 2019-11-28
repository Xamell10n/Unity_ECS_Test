using Enemies;
using LeoECS.Systems;
using Leopotam.Ecs;
using UnityEngine;
using ChangeMovementStateSystem = LeoECS_MultiThreading.Systems.ChangeMovementStateSystem;
using ChangeViewPositionSystem = LeoECS_MultiThreading.Systems.ChangeViewPositionSystem;
using MovementToPositionSystem = LeoECS_MultiThreading.Systems.MovementToPositionSystem;

namespace LeoECS_MultiThreading
{
    [CreateAssetMenu(menuName = "Installers/LeoEcsMultiThreadingData", fileName = "LeoECSMultiThreadingData")]
    public class LeoECSMultiThreadingSceneContext : BaseSceneContext
    {
        public override void InstallBindings()
        {
            BindSpawnData<EnemyController.LeoECSFactory>();
            Container.BindInterfacesAndSelfTo<LeoECSMultiThreadingGameManager>().AsSingle().NonLazy();
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
}