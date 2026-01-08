using UnityEngine;

namespace Abstractions.Interfaces
{
    public interface IDamageable<T>
    {
        public void Damage(T value, GameObject sender);
    }
}