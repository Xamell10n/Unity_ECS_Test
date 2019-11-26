using Unity.Entities;
using UnityEngine;

namespace Enemies.Unity_ECS
{
    public class MovementComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new MovementData() { Speed = 0f, Direction = Vector3.zero});
        }
    }
}