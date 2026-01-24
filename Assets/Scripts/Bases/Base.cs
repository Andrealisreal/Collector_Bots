using System.Collections.Generic;
using Resources;
using UI.Bases;
using Units;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private BaseScanner _baseScanner;
        [SerializeField] private BaseViewStatistics _statistics;
        [SerializeField] private float _radiusCollect;
        [SerializeField] private LayerMask _layerMask;

        private readonly List<Unit> _units = new List<Unit>();

        private BaseTriggerHandler _baseTriggerHandler;
        private ResourceHandler _resourceHandler;
        private BaseStorage _storage;
        private UnitSpawner _unitSpawner;

        private void Awake()
        {
            _unitSpawner = GetComponentInChildren<UnitSpawner>();
            _resourceHandler = new ResourceHandler();
            _baseTriggerHandler = new BaseTriggerHandler(transform.position, _radiusCollect, _layerMask);
            _storage = new BaseStorage();
        }

        private void Start()
        {
            for (var i = 0; i < _unitSpawner.StartQuantity; i++)
                _units.Add(_unitSpawner.Spawn());
        }
        
        private void OnEnable()
        {
            _baseScanner.Detected += OnCollect;
            _baseTriggerHandler.Released += OnRelease;
            _storage.CountChanged += _statistics.UpdateCount;
        }

        private void OnDisable()
        {
            _baseScanner.Detected -= OnCollect;
            _baseTriggerHandler.Released -= OnRelease;
            _storage.CountChanged -= _statistics.UpdateCount;
        }

        private void FixedUpdate() =>
            _baseTriggerHandler.HandleObjects();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radiusCollect);
        }

        private void OnCollect(Resource resource)
        {
            _resourceHandler.Add(resource);

            DispatchUnitToResource();
        }

        private void DispatchUnitToResource()
        {
            foreach (var unit in _units)
            {
                if (unit.IsBusy)
                    continue;

                if (_resourceHandler.TryGetFree(out var freeResource) == false)
                    continue;
                
                unit.MoveToTarget(freeResource.transform);

                return;
            }
        }

        private void OnRelease(Resource resource)
        {
            _storage.ChangeCount();
            _resourceHandler.Release(resource);
        }
    }
}