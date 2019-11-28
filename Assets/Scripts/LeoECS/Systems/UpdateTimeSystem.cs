using LeoECS.Components;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace LeoECS.Systems
{
    public class UpdateTimeSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsWorld _world;
        
        private TimeComponent _timeComponent;

        public UpdateTimeSystem
        (
            [Inject(Id = "Game")] EcsWorld world
        )
        {
            _world = world;
        }
        
        public void Init()
        {
            _world.NewEntityWith(out _timeComponent);
            Run();
        }
        
        public void Run()
        {
            _timeComponent.DeltaTime = Time.deltaTime;
            _timeComponent.RealTimeSinceStartup = Time.realtimeSinceStartup;
        }

    }
}