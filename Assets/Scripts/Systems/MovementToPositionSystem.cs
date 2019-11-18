using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class MovementToPositionSystem : IExecuteSystem
    {
        private readonly InputContext _inputContext;
        private readonly IGroup<GameEntity> _gameGroup;
        private readonly List<GameEntity> _entities;

        public MovementToPositionSystem
        (
            GameContext gameContext,
            InputContext inputContext
        )
        {
            _inputContext = inputContext;
            
            _entities = new List<GameEntity>();
            _gameGroup = gameContext.GetGroup(GameMatcher.Movement);
        }
        
        public void Execute()
        {
            foreach (var entity in _gameGroup.GetEntities(_entities))
            {
                var delta = _inputContext.deltaTime.Value * entity.movement.Direction *
                            entity.movement.Speed;
                var nextPosition = delta + entity.position.GetVector3();
                entity.ReplacePosition(nextPosition.x, nextPosition.y, nextPosition.z);
            }
        }
    }
}