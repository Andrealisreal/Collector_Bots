using Generics.Objects;
using UnityEngine;

namespace Generics.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected ObjectsPool<T> ObjectsPool;
        [SerializeField] protected float MaxSpawnRadius;
        
        private const float MinSpawnRadius = 0;

        public void Spawn()
        {
            var item = ObjectsPool.GetObject();
            
            item.transform.position = GetSpawnPosition() + GetSpawnOffset();
        }

        protected abstract Vector3 GetSpawnPosition();
        
        private Vector3 GetSpawnOffset()
        {
            var x = Random.Range(MinSpawnRadius, MaxSpawnRadius);
            var z = Random.Range(MinSpawnRadius, MaxSpawnRadius);
            
            return new Vector3(x, 0, z);
        }
    }
}
