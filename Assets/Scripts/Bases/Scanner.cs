using UnityEngine;

namespace Bases
{
    public class Scanner : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speedRotate;
        
        private void Update()
        {
            transform.Rotate(Vector3.forward, _speedRotate * Time.deltaTime);
            
            var rayDirection = transform.forward * _maxDistance;
    
            Physics.Raycast(transform.position, rayDirection, out var hit);
            Debug.DrawRay(transform.position, rayDirection, Color.red);
        }
    }
}
