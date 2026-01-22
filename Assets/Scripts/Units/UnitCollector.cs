using System;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        public event Action Raised;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource) == false)
                return;

            CatchUp(resource.transform);
            Raised?.Invoke();
        }

        private void CatchUp(Transform target)
        {
            target.transform.SetParent(transform);
            target.transform.localPosition = _offset;
        }
    }
}