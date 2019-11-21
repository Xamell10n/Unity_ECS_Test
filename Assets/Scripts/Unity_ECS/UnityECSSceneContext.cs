using Enemies;
using Unity.Entities;
using UnityEngine;
using Unity_ECS;

[CreateAssetMenu(menuName = "Installers/UnityECSData", fileName = "UnityECSData")]
public class UnityECSSceneContext : BaseSceneContext
{
    public override void InstallBindings()
    {
        BindSpawnData<EnemyController.UnityECSFactory>();
        Container.BindInterfacesAndSelfTo<UnityECSGameManager>().AsSingle().NonLazy();
        Container.Bind<MovementData>().FromInstance(_movementData)
            .WhenInjectedInto<UnityECSGameManager>();
        Container.BindInstance(World.Active.EntityManager);
    }
}