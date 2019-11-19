using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ChangeViewPositionSystem : ReactiveSystem<GameEntity>
{
    public ChangeViewPositionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public ChangeViewPositionSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.GameObject.transform.position = entity.position.GetVector3();
        }
    }
}