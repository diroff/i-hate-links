using Abstractions.Components.Interaction;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components.Interaction.Casters
{
    public class BoxInteractionCaster2D : InteractionCaster<Collider2D>
    {
        [SerializeField] private Vector2 _size = new Vector2(2f, 2f);
        [SerializeField] private float _angle = 0f;

        private readonly List<Collider2D> _results = new();

        public override IReadOnlyList<Collider2D> GetColliders(LayerMask mask)
        {
            _results.Clear();
            var hits = Physics2D.OverlapBoxAll(Origin.position, _size, _angle, mask);
            _results.AddRange(hits);
            return _results;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(Origin.position, Quaternion.Euler(0, 0, _angle), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, _size);
            Gizmos.matrix = oldMatrix;
        }
    }
}