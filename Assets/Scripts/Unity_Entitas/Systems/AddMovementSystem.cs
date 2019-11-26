using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class AddMovementSystem : ReactiveSystem<GameEntity>
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly InputContext _inputContext;

        public AddMovementSystem
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
            return context.CreateCollector(GameMatcher.MovementState.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var speed = _data.SpeedRange.GetRandom();
                var direction = Helpers.GetRandomAndNormalizeVector3();
                entity.AddMovement(direction, speed);
            }
        }
    }
}