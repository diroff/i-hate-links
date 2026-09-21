using System.Collections.Generic;
using Abstractions.Components.Interaction;
using UnityEngine;

namespace Gameplay.Components.Interaction.Casters
{
    public class SphereInteractionCaster3D : InteractionCaster3D
    {
        [SerializeField] private float _radius = 2f;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.Collide;

        private readonly List<Collider> _results = new();

        public override IReadOnlyList<Collider> GetColliders(LayerMask mask)
        {
            _results.Clear();
            var hits = Physics.OverlapSphere(Origin.position, _radius, mask, _triggerInteraction);
            _results.AddRange(hits);
            return _results;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Origin.position, _radius);
        }
    }
}