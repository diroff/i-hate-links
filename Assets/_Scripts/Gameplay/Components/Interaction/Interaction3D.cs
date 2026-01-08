using Abstractions.Components;
using Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Components.Interaction
{
    public class Interaction3D : InteractionComponent
    {
        protected override void UpdateInteractionTargets()
        {
            var hits = Physics.SphereCastAll(transform.position, InteractionRange, Vector3.forward, 0f, InteractionMask, TriggerMode);
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
                float dist = Vector3.Distance(transform.position, t as MonoBehaviour ? (t as MonoBehaviour).transform.position : transform.position);
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