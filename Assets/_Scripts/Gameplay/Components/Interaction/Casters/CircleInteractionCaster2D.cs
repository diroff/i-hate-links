using System.Collections.Generic;
using Abstractions.Components.Interaction;
using UnityEngine;

namespace Gameplay.Components.Interaction.Casters
{
    public class CircleInteractionCaster2D : InteractionCaster2D
    {
        [SerializeField] private float _radius = 2f;

        private readonly List<Collider2D> _results = new();

        public override IReadOnlyList<Collider2D> GetColliders(LayerMask mask)
        {
            _results.Clear();
            var hits = Physics2D.OverlapCircleAll(Origin.position, _radius, mask);
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