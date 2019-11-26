using Enemies.Systems;
using Entitas;

namespace Unity_Entitas.Systems
{
    public class MovementStateSwitcher : Feature
    {
        public MovementStateSwitcher
        (
            BaseSceneContext.MovementData data,
            IContext<GameEntity> context,
            InputContext inputContext
        )
        {
            Add(new DeleteWaitStateSystem(context, inputContext));
            Add(new DeleteMovementStateSystem(context, inputContext));
            Add(new AddWaitStateSystem(data, inputContext, context));
            Add(new AddMovementStateSystem(data, inputContext, context));
            Add(new AddMovementSystem(data, inputContext, context));
        }
    }
}