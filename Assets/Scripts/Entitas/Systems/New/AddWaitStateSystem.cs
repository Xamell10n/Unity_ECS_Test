using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class AddWaitStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly InputContext _inputContext;

        public AddWaitStateSystem
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
            return context.CreateCollector(GameMatcher.MovementState.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var timeToRemoveWaitState = _inputContext.time.RealTimeSinceStarup +
                                                _data.WaitTimeRange.GetRandom(); 
                entity.AddWaitState(timeToRemoveWaitState);
            }
        }
    }
}