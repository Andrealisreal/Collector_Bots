using System.Collections;
using Generics.Spawners;
using UnityEngine;

namespace Resources
{
    public class ResourceSpawner : Spawner<Resource>
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _delay;

        private WaitForSeconds _wait;

        private void Awake() =>
            _wait = new WaitForSeconds(_delay);

        private void Start() =>
            StartCoroutine(SpawnRoutine());

        protected override Vector3 GetSpawnPosition()
        {
            var randomIndex = Random.Range(0, _spawnPoints.Length);

            return _spawnPoints[randomIndex].position;
        }

        private IEnumerator SpawnRoutine()
        {
            while (enabled)
            {
                yield return _wait;

                Spawn();
            }
        }
    }
}