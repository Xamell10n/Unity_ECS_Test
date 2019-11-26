using System.Collections.Generic;
using Entitas;

namespace Unity_Entitas.Systems
{
    public class DeleteWaitStateSystem : ReactiveSystem<GameEntity>
    {
        private readonly InputContext _inputContext;
        
        public DeleteWaitStateSystem
        (
            IContext<GameEntity> context,
            InputContext inputContext
        ) : base(context)
        {
            _inputContext = inputContext;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.WaitState);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.waitState.FinishMovementStateTime <
                    _inputContext.time.RealTimeSinceStarup)
                {
                    entity.RemoveWaitState();
                }
            }
        }
    }
}