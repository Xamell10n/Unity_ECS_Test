using Enemies;
using GameManagers;
using UnityEngine;

[CreateAssetMenu(menuName = "Installers/EntitasData", fileName = "EntitasData")]
public class EntitasSceneContext : BaseSceneContext
{
    public override void InstallBindings()
    {
        BindSpawnData<EnemyController.EntitasFactory>();
        Container.BindInterfacesAndSelfTo<EntitasGameManager>().AsSingle().NonLazy();
        Container.Bind<MovementData>().FromInstance(_movementData)
            .WhenInjectedInto<EntitasGameManager>();
    }
}