using System;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layerMask;

        private readonly Collider[] _colliders = new Collider[10];

        private bool _hasResource;

        public event Action Raised;

        private void FixedUpdate() =>
            SearchResource();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public void DropResource() =>
            _hasResource = false;

        private void CatchUp(Transform resource)
        {
            resource.SetParent(transform);
            resource.localPosition = _offset;
            Raised?.Invoke();
        }

        private void SearchResource()
        {
            if (_hasResource)
                return;

            var count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders, _layerMask);

            for (var i = 0; i < count; i++)
            {
                if (_colliders[i] == null)
                    continue;

                if (_colliders[i].TryGetComponent(out Resource resource) == false)
                    continue;

                CatchUp(resource.transform);
                _hasResource = true;

                return;
            }
        }
    }
}