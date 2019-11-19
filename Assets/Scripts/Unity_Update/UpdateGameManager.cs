using Enemies;
using UnityEngine;
using Zenject;

namespace GameManagers
{
    public class UpdateGameManager : BaseGameManager, IInitializable
    {
        public UpdateGameManager
        (
            EnemyController.Factory factory,
            int count
        ): base(factory, count) { }
        
        public void Initialize()
        {
            Debug.Log("Initialize");
            for (var i = 0; i < _count; i++)
                _factory.Create();
        }
    }
}