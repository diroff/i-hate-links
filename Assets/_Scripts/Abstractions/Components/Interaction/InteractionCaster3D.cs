using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Components.Interaction
{
    public abstract class InteractionCaster3D : MonoBehaviour
    {
        [SerializeField] protected Transform OriginPoint;

        public Transform Origin => OriginPoint != null ? OriginPoint : transform;

        public abstract IReadOnlyList<Collider> GetColliders(LayerMask mask);

        protected abstract void DrawGizmos();

        protected virtual void OnDrawGizmosSelected()
        {
            DrawGizmos();
        }
    }
}