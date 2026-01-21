using Units;
using UnityEngine;

namespace Resources
{
    [RequireComponent(typeof(Resource))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Resource : MonoBehaviour
    {
        public bool IsRaised { get; private set; }
        public bool IsReserved { get; private set; }

        public Unit ReservedBy { get; private set; }

        public void Reserve(Unit unit)
        {
            IsReserved = true;
            ReservedBy = unit;
        }

        public void SetRaised() =>
            IsRaised = true;

        public void Release()
        {
            IsReserved = false;
            IsRaised = false;
            ReservedBy = null;

            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            transform.SetParent(null);

            gameObject.SetActive(false);
        }
    }
}