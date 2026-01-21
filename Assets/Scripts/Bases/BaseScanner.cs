using System;
using System.Collections;
using Resources;
using UnityEngine;

namespace Bases
{
    public class BaseScanner : MonoBehaviour
    {
        [SerializeField] private float _scanInterval;
        [SerializeField] private float _scanRadius;
        [SerializeField] private LayerMask _layerMask;

        private readonly Collider[] _colliders = new Collider[30];

        private WaitForSeconds _wait;

        public event Action<Resource> Detected;

        private void Awake() =>
            _wait = new WaitForSeconds(_scanInterval);

        private void Start() =>
            StartCoroutine(ScanRoutine());

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _scanRadius);
        }

        private IEnumerator ScanRoutine()
        {
            while (enabled)
            {
                var count = Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, _colliders, _layerMask);

                for (var i = 0; i < count; i++)
                {
                    if (_colliders[i] == null)
                        continue;

                    if (_colliders[i].gameObject.TryGetComponent(out Resource resource) && resource.IsReserved == false)
                        Detected?.Invoke(resource);
                }

                yield return _wait;
            }
        }
    }
}