using System.Collections.Generic;
using UnityEngine;

namespace Generics.Objects
{
    public abstract class ObjectsPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialPoolSize = 10;

        public List<T> Pool { get; } = new List<T>();

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

                return item;
            }

            var newItem = Create();

            newItem.gameObject.SetActive(true);

            return newItem;
        }

        private T Create()
        {
            var item = Instantiate(_prefab, transform);
            item.gameObject.SetActive(false);
            Pool.Add(item);

            return item;
        }
    }
}