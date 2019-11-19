using Enemies;
using GameManagers;
using Zenject;

namespace Unity_ECS
{
    public class UnityECSGameManager : BaseGameManager, IInitializable, ITickable
    {
        public UnityECSGameManager
        (
            EnemyController.Factory factory,
            int count
        ) : base(factory, count)
        {
        }

        public void Initialize()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}