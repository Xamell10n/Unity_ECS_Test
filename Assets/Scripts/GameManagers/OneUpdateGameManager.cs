using System.Collections.Generic;
using Enemies;
using Enemies.Base;
using Enemies.OneUpdate;
using UnityEngine;
using Zenject;

namespace GameManagers
{
    public class OneUpdateGameManager : BaseGameManager, IInitializable, ITickable
    {
        private readonly BaseSceneContext.MovementData _data;
        private readonly List<OneUpdateMovementController> _controllers = new List<OneUpdateMovementController>();

        public OneUpdateGameManager
        (
            EnemyController.Factory factory,
            BaseSceneContext.MovementData data,
            int count
        ) : base(factory, count)
        {
            _data = data;
        }
        
        public void Initialize()
        {
            Debug.Log("Initialize");
            for (var i = 0; i < _count; i++)
            {
                var enemy = _factory.Create();
                var movement = enemy.GetComponent<OneUpdateMovementController>();
                _controllers.Add(movement);
            }
        }

        public void Tick()
        {
            var deltaTime = Time.deltaTime;
            var timeSinceStartup = Time.realtimeSinceStartup;
            foreach (var controller in _controllers)
            {
                if (controller.FinishMovenetStateTime <= timeSinceStartup)
                {
                    float delta;
                    if (controller.IsMoving)
                    {
                        controller.SetSpeed(0f);
                        delta = _data.WaitTimeRange.GetRandom();
                    }
                    else
                    {
                        var speed = _data.SpeedRange.GetRandom();
                        controller.SetSpeed(speed);
                        controller.Direction = Helpers.GetRandomAndNormalizeVector3();
                        delta = _data.MovementTimeRange.GetRandom();
                    }
                    controller.FinishMovenetStateTime = timeSinceStartup + delta;
                }
                else
                {
                    controller.Move(deltaTime);
//                    controller.Rotate(deltaTime);
                }
            }
        }
    }
}