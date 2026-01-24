using Generics.Spawners;
using UnityEngine;

namespace Units
{
    public class UnitSpawner : Spawner<Unit>
    {
        [field: SerializeField] public float StartQuantity { get; private set; }

        [SerializeField] private Unit _prefab;
        [SerializeField] private Transform _spawnPoint;

        protected override Vector3 GetSpawnPosition() =>
            _spawnPoint.position;

        public override Unit Spawn()
        {
            var item = Instantiate(_prefab);
            
            item.transform.position = GetSpawnPosition() + GetSpawnOffset();
            item.SetBasePosition(transform);
            
            return item;
        }
    }
}