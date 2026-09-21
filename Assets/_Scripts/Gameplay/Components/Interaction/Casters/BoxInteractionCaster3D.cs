using Abstractions.Components.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components.Interaction.Casters
{
    public class BoxInteractionCaster3D : InteractionCaster<Collider>
    {
        [SerializeField] private Vector3 _halfExtents = Vector3.one;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.Collide;

        private readonly List<Collider> _results = new();

        public override IReadOnlyList<Collider> GetColliders(LayerMask mask)
        {
            _results.Clear();
            var hits = Physics.OverlapBox(Origin.position, _halfExtents, Origin.rotation, mask, _triggerInteraction);
            _results.AddRange(hits);
            return _results;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(Origin.position, Origin.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, _halfExtents * 2f);
            Gizmos.matrix = oldMatrix;
        }
    }
}