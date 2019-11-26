using Enemies;
using Enemies.Unity_ECS.Systems;
using GameManagers;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace Unity_ECS
{
    public class UnityECSGameManager : BaseGameManager, IInitializable, ITickable
    {
        private readonly EnemyController.UnityECSFactory _factory;
        private readonly EntityManager _entityManager;
        private readonly int _count;

        public UnityECSGameManager
        (
            EnemyController.UnityECSFactory factory,
            EntityManager entityManager,
            BaseSceneContext.MovementData data,
            int count
        ) : base(factory, count)
        {
            _factory = factory;
            _entityManager = entityManager;
            _count = count;

            _entityManager.World.CreateSystem<ChangeMovementStateSystem>(data);
        }

        public void Initialize()
        {
            Debug.Log("Initialize");

            for (var i = 0; i < _count; i++)
                _factory.Create();
        }

        public void Tick()
        {
            _entityManager.World.GetOrCreateSystem<ChangeMovementStateSystem>().Update();
        }
    }
}