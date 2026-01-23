using System.Collections.Generic;
using UnityEngine;

namespace Generics.Objects
{
    public abstract class ObjectsPool<T> : MonoBehaviour where T : PoolableObject<T>
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialPoolSize = 10;

        private readonly List<T> _pool = new();

        public IReadOnlyList<T> Pool => _pool;

        private void Awake()
        {
            for (var i = 0; i < _initialPoolSize; i++)
                Create();
        }

        public T GetObject()
        {
            foreach (var item in Pool)
            {
                if (item.gameObject.activeInHierarchy)
                    continue;

                item.gameObject.SetActive(true);
                item.Released += OnObjectReleased;

                return item;
            }

            var newItem = Create();

            newItem.gameObject.SetActive(true);
            newItem.Released += OnObjectReleased;

            return newItem;
        }
        
        protected virtual void OnObjectReleased(T item)
        {
            item.Released -= OnObjectReleased;
            item.gameObject.SetActive(false);
        }

        private T Create()
        {
            var item = Instantiate(_prefab, transform);
            item.gameObject.SetActive(false);
            _pool.Add(item);

            return item;
        }
    }
}