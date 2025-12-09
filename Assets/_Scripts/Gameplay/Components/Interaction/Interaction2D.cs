using Abstractions.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Components.Interaction
{
    public class Interaction2D : InteractionComponent
    {
        protected override void UpdateInteractionTargets()
        {
            var hits = Physics2D.CircleCastAll(transform.position, InteractionRange,
                                              Vector2.zero, 0f, InteractionMask);

            var found = new HashSet<IInteractable>();

            foreach (var hit in hits)
            {
                if (!hit.collider.TryGetComponent<IInteractable>(out var interactable)) continue;
                if (!interactable.CanInteract(gameObject)) continue;

                found.Add(interactable);
                if (!TargetsInRange.Contains(interactable))
                    AddTarget(interactable);
            }

            for (int i = TargetsInRange.Count - 1; i >= 0; i--)
            {
                if (!found.Contains(TargetsInRange[i]))
                    RemoveTarget(TargetsInRange[i]);
            }

            IInteractable best = null;
            float bestDist = float.MaxValue;

            foreach (var t in TargetsInRange)
            {
                var mb = t as MonoBehaviour;
                if (mb == null)
                    continue;

                float dist = Vector2.Distance(transform.position, mb.transform.position);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = t;
                }
            }

            SetCurrentTarget(best);
        }

        protected override void DrawInteractionGizmo()
        {
            Gizmos.DrawWireSphere(transform.position, InteractionRange);
        }
    }
}