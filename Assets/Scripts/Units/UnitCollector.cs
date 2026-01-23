using System;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        
        private Transform _resource;

        public event Action Raised;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource) == false)
                return;
            
            _resource = resource.transform;
            Raised?.Invoke();
        }

        public void CatchUp()
        {
            _resource.transform.SetParent(transform);
            _resource.transform.localPosition = _offset;
        }
    }
}