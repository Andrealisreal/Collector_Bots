using System.Collections.Generic;
using UnityEngine;

namespace Generics.Objects
{
    public abstract class ObjectsPool<T> : MonoBehaviour where T : PoolableObject<T>
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialPoolSize = 10;

        private readonly List<T> _pool = new();

        private void Awake()
        {
            for (var i = 0; i < _initialPoolSize; i++)
                Create();
        }

        public T GetObject()
        {
            T item = null;

            foreach (var obj in _pool)
            {
                if (obj.gameObject.activeInHierarchy)
                    continue;

                item = obj;
                break;
            }

            if (item == null)
                item = Create();
            
            item.gameObject.SetActive(true);

            item.Released += OnObjectReleased;

            return item;
        }


        private static void OnObjectReleased(T item)
        {
            item.Released -= OnObjectReleased;
            item.transform.position = Vector3.zero;
            item.gameObject.SetActive(false);
        }

        private T Create()
        {
            var item = Instantiate(_prefab);
            item.gameObject.SetActive(false);
            _pool.Add(item);

            return item;
        }
    }
}