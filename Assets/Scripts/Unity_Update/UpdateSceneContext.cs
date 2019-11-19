using Enemies;
using Enemies.Update;
using GameManagers;
using UnityEngine;

[CreateAssetMenu(menuName = "Installers/UpdateData", fileName = "UpdateData")]
public class UpdateSceneContext : BaseSceneContext
{
    public override void InstallBindings()
    {
        BindSpawnData<EnemyController.Factory>();
        Container.BindInterfacesAndSelfTo<UpdateGameManager>().AsSingle().NonLazy();
        Container.Bind<MovementData>().FromInstance(_movementData)
            .WhenInjectedInto<UpdateEnemyMovementController>();
    }
}