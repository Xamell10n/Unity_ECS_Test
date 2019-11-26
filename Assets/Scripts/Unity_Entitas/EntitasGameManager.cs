using Enemies;
using Enemies.Systems;
using Entitas;
using UnityEngine;
using Unity_Entitas.Systems;
using Zenject;

namespace GameManagers
{
    public class EntitasGameManager : BaseGameManager, IInitializable, ITickable
    {
        private readonly Systems _systems;

        public EntitasGameManager
        (
            BaseSceneContext.MovementData data,
            EnemyController.EntitasFactory factory,
            int count
        ) : base(factory, count)
        {
            var contexts = Contexts.sharedInstance;
            var gameContext = contexts.game;
            var inputContext = contexts.input;

            _systems = new Systems()
                .Add(new UpdateTimeSystem(inputContext))
                .Add(new ChangeMovementStateSystem(data, gameContext, inputContext))
//                .Add(new MovementStateSwitcher(data, gameContext, inputContext))
                .Add(new MovementToPositionSystem(gameContext, inputContext))
                .Add(new ChangeViewPositionSystem(gameContext));
        }

        public void Initialize()
        {
            Debug.Log("Initialize");

            for (var i = 0; i < _count; i++)
                _factory.Create();
            _systems.Initialize();
        }

        public void Tick()
        {
            _systems.Execute();
        }
    }
}