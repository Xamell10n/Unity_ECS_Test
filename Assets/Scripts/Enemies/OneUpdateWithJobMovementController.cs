using Enemies.Base;
using UnityEngine;

namespace Enemies
{
    public class OneUpdateWithJobMovementController : BaseEnemyMovementController
    {
        [HideInInspector] public float FinishMovenetStateTime;
        [HideInInspector] public float Speed;
        [HideInInspector] public Vector3 Direction;

        public bool IsMoving => Speed > 0;
    }
}