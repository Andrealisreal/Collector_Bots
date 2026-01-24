using System;
using UnityEngine;

namespace Generics.Objects
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class PoolableObject<T> : MonoBehaviour where T : PoolableObject<T>
    {
        public event Action<T> Released;

        public virtual void Release() =>
            Released?.Invoke((T)this);
    }
}
