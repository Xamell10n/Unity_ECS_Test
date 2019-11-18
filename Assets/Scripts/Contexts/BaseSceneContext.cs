using System;
using Enemies;
using GameManagers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public abstract class BaseSceneContext : ScriptableObjectInstaller
{
    [SerializeField] protected SpawnData _spawnData;
    [SerializeField] protected MovementData _movementData;

    protected void BindSpawnData<T>() where T : EnemyController.Factory
    {
        Container.BindInstance(_spawnData.Count).WhenInjectedInto<BaseGameManager>();
        Container.BindFactory<EnemyController, T>()
            .FromComponentInNewPrefabResource($"Enemies/{_spawnData.PrefabName}").UnderTransformGroup("Enemies");
        Container.BindInstance(new EnemyController.Factory.Data()
        {
            Center = _spawnData.StartPosition,
            Seed = _spawnData.StartPositionDelta,
            AngleSeed = _spawnData.StartAngle
        }).WhenInjectedInto<T>();
    }

    [Serializable]
    public class SpawnData
    {
        public int Count = 10000;
        public string PrefabName;
        public Vector3 StartPosition = Vector3.zero;
        public Vector3 StartPositionDelta = Vector3.one * 10;
        public float StartAngle = 45;
    }

    [Serializable]
    public class MovementData
    {
        public Range SpeedRange = new Range(2, 4);
        public Range MovementTimeRange = new Range(2, 4);
        public Range WaitTimeRange = new Range(0, 3);

        [Serializable]
        public class Range
        {
            [SerializeField] private float _min;
            [SerializeField] private float _max;

            public Range(float min, float max)
            {
                _min = min;
                _max = max;
            }

            public float GetRandom()
            {
                var result = Random.Range(_min, _max);
                return result;
            }

            public override string ToString()
            {
                var result = $"Min = {_min}, Max = {_max}";
                return result;
            }
        }
    }
}