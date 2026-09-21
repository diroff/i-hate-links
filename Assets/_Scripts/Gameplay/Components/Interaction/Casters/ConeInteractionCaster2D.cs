using System.Collections.Generic;
using Abstractions.Components.Interaction;
using UnityEngine;

namespace Gameplay.Components.Interaction.Casters
{
    public class ConeInteractionCaster2D : InteractionCaster2D
    {
        [SerializeField] private float _radius = 3f;
        [SerializeField][Range(0f, 360f)] private float _angle = 60f;

        private readonly List<Collider2D> _results = new();

        public override IReadOnlyList<Collider2D> GetColliders(LayerMask mask)
        {
            _results.Clear();

            var hits = Physics2D.OverlapCircleAll(Origin.position, _radius, mask);
            Vector2 forward = Origin.right;

            foreach (var hit in hits)
            {
                if (hit == null)
                    continue;

                Vector2 directionToTarget = (hit.transform.position - Origin.position);
                float distance = directionToTarget.magnitude;

                if (distance > _radius)
                    continue;

                float angleToTarget = Vector2.Angle(forward, directionToTarget);

                if (angleToTarget <= _angle * 0.5f)
                {
                    _results.Add(hit);
                }
            }

            return _results;
        }

        protected override void DrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Vector3 originPos = Origin.position;
            Vector3 forward = Origin.right;

            float halfAngle = _angle * 0.5f;
            Vector3 rightBoundary = Quaternion.Euler(0, 0, -halfAngle) * forward;
            Vector3 leftBoundary = Quaternion.Euler(0, 0, halfAngle) * forward;

            Gizmos.DrawRay(originPos, rightBoundary * _radius);
            Gizmos.DrawRay(originPos, leftBoundary * _radius);

            int segments = 20;
            Vector3 previousPoint = originPos + rightBoundary * _radius;

            for (int i = 1; i <= segments; i++)
            {
                float currentAngle = -halfAngle + (_angle / segments) * i;
                Vector3 currentDirection = Quaternion.Euler(0, 0, currentAngle) * forward;
                Vector3 currentPoint = originPos + currentDirection * _radius;

                Gizmos.DrawLine(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }
    }
}