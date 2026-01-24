using System;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMover : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Awake() =>
            _agent = GetComponent<NavMeshAgent>();

        public void Move(Transform target) =>
            _agent.SetDestination(target.position);
    }
}