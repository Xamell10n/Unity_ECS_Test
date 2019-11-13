using Enemies.Base;
using UnityEngine;
using Zenject;

namespace Enemies.Update
{
    public class UpdateEnemyMovementController : BaseEnemyMovementController
    {
        private BaseSceneContext.MovementData _movementData;
        private float _finishMovingStateTime;
        private Vector3 _direction;

        [Inject]
        public void Inject(BaseSceneContext.MovementData movementData)
        {
            _movementData = movementData;
        }

        private void Update()
        {
            if (_finishMovingStateTime <= Time.realtimeSinceStartup)
            {
                float delta;
                if (_speed > 0)
                {
                    _speed = 0f;
                    delta = _movementData.WaitTimeRange.GetRandom();
                }
                else
                {
                    _speed = _movementData.SpeedRange.GetRandom();
                    _direction = Helpers.GetRandomAndNormalizeVector3();
                    delta = _movementData.MovementTimeRange.GetRandom();
                }
                _finishMovingStateTime = Time.realtimeSinceStartup + delta;
            }
            else
            {
                transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
//              transform.Rotate(Vector3.up, _angleSpeed * Time.deltaTime);
            }
        }
    }
}