using System;
using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    public class BaseTriggerHandler
    {
        public event Action<Resource> Released;
        
        public void OnEnter(Collider other)
        {
            OnEnterResource(other);
            OnEnterUnit(other);
        }

        private void OnEnterResource(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource) == false)
                return;

            Released?.Invoke(resource);
            resource.Release();
        }

        private static void OnEnterUnit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Unit unit) == false || unit.IsAvailable == false)
                return;
            
            unit.Release();
        }
    }
}