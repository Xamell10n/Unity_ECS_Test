using Enemies;
using GameManagers;
using UnityEngine;

[CreateAssetMenu(menuName = "Installers/OneUpdateWithJobData", fileName = "OneUpdateWithJobData")]
public class OneUpdateWithJobSceneContext : BaseSceneContext
{
    public override void InstallBindings()
    {
        BindSpawnData<EnemyController.Factory>();
        Container.BindInterfacesAndSelfTo<OneUpdateWithJobGameManager>().AsSingle().NonLazy();
        Container.Bind<MovementData>().FromInstance(_movementData)
            .WhenInjectedInto<OneUpdateWithJobGameManager>();
    }
}