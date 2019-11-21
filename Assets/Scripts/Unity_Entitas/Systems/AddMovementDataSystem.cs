using System.Collections.Generic;
using Entitas;

namespace Enemies.Systems
{
    public class AddMovementDataSystem : ReactiveSystem<GameEntity>
    {
        public AddMovementDataSystem(IContext<GameEntity> context) : base(context)
        {
        }

        public AddMovementDataSystem(ICollector<GameEntity> collector) : base(collector)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.Movement.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            
        }
    }
}