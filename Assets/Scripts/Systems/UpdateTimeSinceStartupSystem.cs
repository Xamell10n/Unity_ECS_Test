using Entitas;
using UnityEngine;

namespace Enemies.Systems
{
    public class UpdateTimeSinceStartupSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly InputContext _inputContext;

        public UpdateTimeSinceStartupSystem
        (
            InputContext inputContext
        )
        {
            _inputContext = inputContext;
        }
        
        public void Initialize()
        {
            Execute();
        }

        public void Execute()
        {
            _inputContext.ReplaceTimeSinceStartup(Time.realtimeSinceStartup);
        }

    }
}