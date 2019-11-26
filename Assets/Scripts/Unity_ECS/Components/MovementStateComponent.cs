using Unity.Entities;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    public class MovementStateComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new MovementStateData { FinishMovementStateTime = 0});
        }
    }
}