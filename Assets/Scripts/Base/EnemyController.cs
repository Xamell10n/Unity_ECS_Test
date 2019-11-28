using LeoECS.Components;
using Leopotam.Ecs;
using Unity.Entities;
using UnityEngine;
using Zenject;
using MovementStateComponent = LeoECS.Components.MovementStateComponent;
using PositionComponent = LeoECS.Components.PositionComponent;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<EnemyController>
        {
            private readonly Data _data;

            public Factory
            (
                Data data
            )
            {
                _data = data;
            }
            
            public override EnemyController Create()
            {
                var result = base.Create();
                SetStartPosition(_data.Center, _data.Seed, result);
                SetStartRotation(_data.AngleSeed, result);
                return result;
            }
            
            private void SetStartPosition(Vector3 center, Vector3 seed, Component component)
            {
                var startPositionX = center.x + Random.Range(-seed.x, seed.x);
                var startPositionY = center.y + Random.Range(-seed.y, seed.y);
                var startPositionZ = center.z + Random.Range(-seed.z, seed.z);
            
                component.transform.position = new Vector3(startPositionX, startPositionY, startPositionZ);
            }

            private void SetStartRotation(float angleSeed, Component component)
            {
                var startAngle = Random.Range(-angleSeed, angleSeed);
                
                component.transform.rotation = Quaternion.Euler(0, startAngle, 0);
            }
        
            public class Data
            {
                public Vector3 Center;
                public Vector3 Seed;
                public float AngleSeed;
            }
        }

        public class EntitasFactory : Factory
        {
            private readonly GameContext _context;
            
            public EntitasFactory
            (
                Data data
            ) : base(data)
            {
                _context = Contexts.sharedInstance.game;
            }

            public override EnemyController Create()
            {
                var result = base.Create();
                var entity = _context.CreateEntity();
                entity.AddView(result.gameObject);
//                entity.AddWaitState(0);
                entity.AddMovementState(0);
                var position = result.transform.position;
                entity.AddPositionVector(position);
                return result;
            }
        }
        
        public class UnityECSFactory : Factory
        {
            private readonly EntityManager _entityManager;

            public UnityECSFactory
            (
                Data data,
                EntityManager entityManager
            ) : base(data)
            {
                _entityManager = entityManager;
            }

            public override EnemyController Create()
            {
                var result = base.Create();
//                var entity = _entityManager.Instantiate(result.gameObject);
//                _entityManager.AddComponent<MovementStateData>(entity);
//                _entityManager.AddComponent<Translation>(entity);
                return result;
            }
        }
        
        public class LeoECSFactory : Factory
        {
            private readonly EcsWorld _world;

            public LeoECSFactory
            (
                Data data,
                [Inject(Id = "Game")] EcsWorld world
            ) : base(data)
            {
                _world = world;
            }

            public override EnemyController Create()
            {
                var result = base.Create();
                var entity = _world.NewEntity();
                entity.Set<ViewComponent>().Transform = result.transform;
                entity.Set<PositionComponent>().Value = result.transform.position;
                entity.Set<MovementStateComponent>().FinishMovementStateTime = 0;
                return result;
            }
        }
    }
}