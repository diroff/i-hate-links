using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Interfaces
{
    public interface IInteractionCaster<TCollider> where TCollider : Component
    {
        public Transform Origin { get; }
        public IReadOnlyList<TCollider> GetColliders(LayerMask mask);
    }
}