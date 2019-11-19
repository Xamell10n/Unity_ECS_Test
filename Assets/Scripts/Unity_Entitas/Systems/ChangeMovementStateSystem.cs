using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class ChangeMovementStateSystem : IExecuteSystem
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly InputContext _inputContext;
        private readonly IGroup<GameEntity> _gameGroup;
        private readonly List<GameEntity> _entities;

        public ChangeMovementStateSystem
        (
            BaseSceneContext.MovementData data,
            GameContext gameContext,
            InputContext inputContext
        )
        {
            _data = data;
            _inputContext = inputContext;

            _entities = new List<GameEntity>();
            _gameGroup = gameContext.GetGroup(GameMatcher.MovementState);
        }

        public void Execute()
        {
            foreach (var entity in _gameGroup.GetEntities(_entities))
            {
                if (entity.movementState.FinishMovementStateTime <= _inputContext.timeSinceStartup.Value)
                {
                    float delta;
                    if (entity.hasMovement)
                    {
                        entity.RemoveMovement();
                        delta = _data.WaitTimeRange.GetRandom();
                    }
                    else
                    {
                        var speed = _data.SpeedRange.GetRandom();
                        var direction = Helpers.GetRandomAndNormalizeVector3();
                        entity.ReplaceMovement(direction, speed);
                        delta = _data.MovementTimeRange.GetRandom();
                    }
                    entity.ReplaceMovementState(_inputContext.timeSinceStartup.Value + delta);
                }
            }
        }
    }
}