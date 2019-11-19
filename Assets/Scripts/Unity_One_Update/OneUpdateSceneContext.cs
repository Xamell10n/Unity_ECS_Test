using Enemies;
using GameManagers;
using UnityEngine;

    [CreateAssetMenu(menuName = "Installers/OneUpdateData", fileName = "OneUpdateData")]
    public class OneUpdateSceneContext : BaseSceneContext
    {
        public override void InstallBindings()
        {
            BindSpawnData<EnemyController.Factory>();
            Container.BindInterfacesAndSelfTo<OneUpdateGameManager>().AsSingle().NonLazy();
            Container.Bind<MovementData>().FromInstance(_movementData)
                .WhenInjectedInto<OneUpdateGameManager>();
        }
    }