using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class MovementToPositionSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly GameContext _context;
        private readonly InputContext _inputContext;
        private GameEntity[] _entities;

        public MovementToPositionSystem
        (
            GameContext context,
            InputContext inputContext
        )
        {
            _context = context;
            _inputContext = inputContext;
        }
        
        public void Execute()
        {
            _entities = _context.GetGroup(GameMatcher.Movement).GetEntities();
            foreach (var entity in _entities)
            {
                var delta = _inputContext.deltaTime.Value * entity.movement.Direction *
                            entity.movement.Speed;
                var nextPosition = delta + entity.position.GetVector3();
                entity.ReplacePosition(nextPosition.x, nextPosition.y, nextPosition.z);
            }
        }

        public void Initialize()
        {
//            _entities = _context.GetGroup(GameMatcher.Movement).GetEntities();
        }
    }
}