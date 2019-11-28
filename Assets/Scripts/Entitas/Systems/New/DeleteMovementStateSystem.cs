using System.Collections.Generic;
using Entitas;

namespace Unity_Entitas.Systems
{
    public class DeleteMovementStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly InputContext _inputContext;
        
        public DeleteMovementStateSystem
        (
            IContext<GameEntity> context,
            InputContext inputContext
        ) : base(context)
        {
            _inputContext = inputContext;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MovementState);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.movementState.FinishMovementStateTime <
                    _inputContext.time.RealTimeSinceStarup)
                {
                    entity.RemoveMovementState();
                }
            }
        }
    }
}