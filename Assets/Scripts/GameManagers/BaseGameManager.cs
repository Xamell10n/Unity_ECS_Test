using Enemies;

namespace GameManagers
{
    public abstract class BaseGameManager
    {
        protected readonly EnemyController.Factory _factory;
        protected readonly int _count;

        public BaseGameManager
        (
            EnemyController.Factory factory,
            int count
        )
        {
            _factory = factory;
            _count = count;
        }
    }
}