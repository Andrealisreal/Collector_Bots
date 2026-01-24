using Generics.Objects;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(UnitMover))]
    public class Unit : PoolableObject<Unit>
    {
        private UnitMover _mover;
        private Transform _basePosition;
        private UnitCollector _collector;

        public bool IsBusy { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<UnitMover>();
            _collector = GetComponentInChildren<UnitCollector>();
        }

        private void OnEnable() =>
            _collector.Raised += ReturnToBase;

        private void OnDisable() =>
            _collector.Raised -= ReturnToBase;

        public void MoveToTarget(Transform target)
        {
            _collector.DropResource();
            IsBusy = true;
            _mover.Move(target);
        }
        
        public void SetBasePosition(Transform basePosition) =>
            _basePosition = basePosition;

        public override void Release() =>
            IsBusy = false;

        private void ReturnToBase() =>
            _mover.Move(_basePosition);
    }
}