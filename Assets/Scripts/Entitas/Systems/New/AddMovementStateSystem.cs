using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class AddMovementStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly InputContext _inputContext;

        public AddMovementStateSystem
        (
            BaseSceneContext.MovementData data,
            InputContext inputContext,
            IContext<GameEntity> context
        ) : base(context)
        {
            _data = data;
            _inputContext = inputContext;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.WaitState.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var timeToRemoveMovementState = _inputContext.time.RealTimeSinceStarup +
                                                _data.MovementTimeRange.GetRandom(); 
                entity.AddMovementState(timeToRemoveMovementState);
            }
        }
    }
}