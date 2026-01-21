using Resources;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(UnitMover))]
    public class Unit : MonoBehaviour
    {
        private UnitMover _mover;
        private Transform _basePosition;
        private UnitCollector _collector;

        public bool IsBusy { get; private set; }
        public bool IsAvailable { get; private set; }

        private void Awake()
        {
            _mover = GetComponent<UnitMover>();
            _collector = GetComponentInChildren<UnitCollector>();
        }

        private void OnEnable() =>
            _collector.Raised += MoveToBase;

        private void OnDisable() =>
            _collector.Raised -= MoveToBase;

        public void MoveToTarget(Resource resource)
        {
            IsBusy = true;
            _mover.Move(resource.transform);
        }

        public void SetBasePosition(Transform basePosition) =>
            _basePosition = basePosition;

        public void Release()
        {
            IsBusy = false;
            IsAvailable = false;
        }

        public void ReturnToBase() =>
            IsAvailable = true;

        private void MoveToBase() =>
            _mover.Move(_basePosition);
    }
}