using System;
using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseTriggerHandler
    {
        private readonly Collider[] _colliders =  new Collider[10];
        
        private readonly Vector3 _basePosition;
        private readonly LayerMask _layerMask;
        private readonly float _radius;

        public BaseTriggerHandler(Vector3 basePosition, float radius, LayerMask layerMask)
        {
            _basePosition = basePosition;
            _radius = radius;
            _layerMask = layerMask;
        }
        
        public event Action<Resource> Released;
        
        public void HandleObjects()
        {
            var count = Physics.OverlapSphereNonAlloc(_basePosition, _radius, _colliders, _layerMask);

            for (var i = 0; i < count; i++)
            {
                if (_colliders[i] == null)
                    continue;

                if (_colliders[i].gameObject.TryGetComponent(out Unit unit))
                    unit.Release();

                if (_colliders[i].gameObject.TryGetComponent(out Resource resource) == false)
                    continue;
                
                resource.Release();
                Released?.Invoke(resource);
            }
        }
    }
}