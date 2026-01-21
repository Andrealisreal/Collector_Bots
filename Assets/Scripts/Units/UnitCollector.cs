using System;
using Resources;
using UnityEngine;

namespace Units
{
    public class UnitCollector : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;

        private Unit _unit;

        public event Action Raised;

        private void Awake() =>
            _unit = GetComponentInParent<Unit>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Resource resource))
                if (resource.ReservedBy == _unit && resource.IsRaised == false)
                {
                    resource.SetRaised();
                    CatchUp(resource.transform);
                    _unit.ReturnToBase();
                    Raised?.Invoke();
                }
        }

        private void CatchUp(Transform target)
        {
            target.transform.SetParent(transform);
            target.transform.position += _offset;
        }
    }
}