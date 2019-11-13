using Enemies.Base;
using UnityEngine;

namespace Enemies.OneUpdate
{
    public class OneUpdateMovementController : BaseEnemyMovementController
    {
//        [HideInInspector] public float StartMovementStateTime;
        [HideInInspector] public float FinishMovenetStateTime;
        [HideInInspector] public Vector3 Direction;
        
        public bool IsMoving => _speed > 0;
        
        public void Move(float delta)
        {
            transform.Translate(Direction * _speed * delta, Space.World);
        }

        public void Rotate(float delta)
        {
            transform.Rotate(Vector3.up, _angleSpeed * delta);
        }

        public void SetSpeed(float speed, float angleSpeed = 0f)
        {
            _speed = speed;
            _angleSpeed = angleSpeed;
        }
    }
}