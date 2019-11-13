using Entitas;
using UnityEngine;

namespace Enemies.Systems
{
    public class ChangeMovementStateSystem : IExecuteSystem
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly GameContext _context;

        public ChangeMovementStateSystem
        (
            BaseSceneContext.MovementData data,
            GameContext context
        )
        {
            _data = data;
            _context = context;
        }

        public void Execute()
        {
            foreach (var entity in _context.GetGroup(GameMatcher.MovementState).GetEntities())
            {
                if (entity.movementState.FinishMovementStateTime <= Time.realtimeSinceStartup)
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
                    entity.ReplaceMovementState(Time.realtimeSinceStartup + delta);
                }
            }
        }
    }
}