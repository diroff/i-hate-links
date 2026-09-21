using Abstractions.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Components.Interaction
{
    public abstract class InteractionCaster<TCollider> : MonoBehaviour, IInteractionCaster<TCollider> where TCollider : Component
    {
        [SerializeField] protected Transform OriginPoint;

        public Transform Origin => OriginPoint != null ? OriginPoint : transform;

        public abstract IReadOnlyList<TCollider> GetColliders(LayerMask mask);

        protected abstract void DrawGizmos();

        protected virtual void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }
    }
}