using LeoECS.Components;
using Leopotam.Ecs;

namespace LeoECS.Systems
{
    public class ChangeViewPositionSystem : IEcsRunSystem
    {
        private EcsFilter<PositionComponent, ViewComponent> _positionToViewFilter = null;
        
        public void Run()
        {
            foreach (var i in _positionToViewFilter)
            {
                var position = _positionToViewFilter.Get1[i];
                var view = _positionToViewFilter.Get2[i];
                view.Transform.position = position.Value;
            }
        }
    }
}