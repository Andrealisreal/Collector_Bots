using System;
using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    [Serializable]
    public class BaseTriggerHandler
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radius;
        
        private Vector3 _basePosition;
        
        private Collider[] _colliders;

        public BaseTriggerHandler(Vector3 basePosition)
        {
            _basePosition = basePosition;
        }
        
        public float Radius => _radius;
        
        public event Action<Resource> Released;
        
        public void OnEnter(Collider other)
        {
            EnterResource(other);
        }

        private void EnterResource(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource) == false)
                return;

            Released?.Invoke(resource);
            resource.Release();
            Debug.Log("Resource enter");
        }

        public void EnterUnit()
        {
            var count = Physics.OverlapSphereNonAlloc(_basePosition, _radius, _colliders, _layerMask);

            for (var i = 0; i < count; i++)
            {
                if (_colliders[i] == null)
                    continue;

                if (_colliders[i].gameObject.TryGetComponent(out Unit unit) == false)
                    return;
                
                unit.Release();
            }
        }
    }
}