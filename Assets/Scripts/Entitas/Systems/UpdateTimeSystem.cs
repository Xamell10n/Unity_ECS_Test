using Entitas;
using UnityEngine;

namespace Enemies.Systems
{
    public class UpdateTimeSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly InputContext _context;

        public UpdateTimeSystem
        (
            InputContext context
        )
        {
            _context = context;
        }
        
        public void Initialize()
        {
            Execute();
        }

        public void Execute()
        {
            _context.ReplaceTime(Time.deltaTime, Time.realtimeSinceStartup);
        }
    }
}