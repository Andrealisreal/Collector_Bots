using Resources;
using UI.Bases;
using Units;
using UnityEngine;

namespace Bases
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private BaseScanner _baseScanner;
        [SerializeField] private UnitPool _unitPool;
        [SerializeField] private BaseViewStatistics _statistics;
        
        private BaseTriggerHandler _baseTriggerHandler;
        private ResourceHandler _resourceHandler;
        private BaseStorage _storage;

        private void Awake()
        {
            _resourceHandler = new ResourceHandler();
            _baseTriggerHandler = new BaseTriggerHandler();
            _storage = new BaseStorage();
        }

        private void OnTriggerEnter(Collider other) =>
            _baseTriggerHandler.OnEnter(other);

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

        private void OnCollect(Resource resource)
        {
            _resourceHandler.Add(resource);
            
            SendResource();
        }

        private void SendResource()
        {
            foreach (var unit in _unitPool.Pool)
            {
                if (unit.IsBusy || unit.IsHold)
                    continue;

                if(_resourceHandler.TryGetFree(out var freeResource) == false)
                    continue;
                
                unit.MoveToTarget(freeResource.transform.position);
                unit.SetBasePosition(transform.position);

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