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
        public bool IsAvailable { get; private set; }
        public bool IsHold { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<UnitMover>();
            _collector = GetComponentInChildren<UnitCollector>();
        }

        private void OnEnable() =>
            _collector.Raised += MoveToBase;

        private void OnDisable() =>
            _collector.Raised -= MoveToBase;

        public void MoveToTarget(Transform target)
        {
            IsBusy = true;
            _mover.Move(target);
        }

        public void SetBasePosition(Transform basePosition) =>
            _basePosition = basePosition;

        public override void Release()
        {
            base.Release();
            
            IsHold = false;
            IsBusy = false;
            IsAvailable = false;
            
            Debug.Log("Unit Released");
        }

        private void MoveToBase()
        {
            IsAvailable = true;
            IsHold = true;
            _mover.Move(_basePosition);
        }
    }
}