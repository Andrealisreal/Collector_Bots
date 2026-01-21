using System;
using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseStorage : MonoBehaviour
    {
        private int _count;

        public event Action<int> CountChanged;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource))
            {
                _count++;
                CountChanged?.Invoke(_count);
                resource.Release();
            }

            if (other.gameObject.TryGetComponent(out Unit unit) && unit.IsAvailable)
                unit.Release();
        }
    }
}