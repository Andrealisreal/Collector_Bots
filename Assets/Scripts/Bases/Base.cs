using Resources;
using Units;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private BaseScanner _baseScanner;
        [SerializeField] private UnitPool _unitPool;

        private void OnEnable() =>
            _baseScanner.Detected += OnCollect;

        private void OnDisable() =>
            _baseScanner.Detected -= OnCollect;

        private void OnCollect(Resource resource)
        {
            foreach (var unit in _unitPool.Pool)
            {
                if (unit.IsBusy || resource.IsReserved)
                    continue;

                resource.Reserve(unit);
                unit.MoveToTarget(resource);
                unit.SetBasePosition(transform);

                return;
            }
        }
    }
}