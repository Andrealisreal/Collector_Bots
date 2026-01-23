using Generics.Objects;
using UnityEngine;

namespace Resources
{
    [RequireComponent(typeof(SphereCollider))]
    public class Resource : PoolableObject<Resource>
    {
        public override void Release()
        {
            base.Release();

            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            transform.SetParent(null);
        }
    }
}