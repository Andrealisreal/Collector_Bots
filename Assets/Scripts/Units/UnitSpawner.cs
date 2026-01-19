using Generics.Spawners;
using UnityEngine;

namespace Units
{
    public class UnitSpawner : Spawner<Unit>
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _startQuantity;

        private void Start()
        {
            for(var i = 0; i < _startQuantity; i++)
                Spawn();
        }

        protected override Vector3 GetSpawnPosition() =>
            _spawnPoint.position;
    }
}