using System.Collections.Generic;
using Abstractions.Components.Interaction;
using Abstractions.Interfaces;
using UnityEngine;

namespace Abstractions.Components
{
    public abstract class TypedInteractionComponent<TCaster, TCollider> : InteractionComponent
        where TCaster : Component
        where TCollider : Component
    {
        [SerializeField] protected LayerMask InteractionMask = ~0;

        protected abstract TCaster Caster { get; }
        protected abstract Transform Origin { get; }

        private readonly HashSet<IInteractable> _foundTargets = new();

        protected virtual void Update()
        {
            UpdateInteractionTargets();
        }

        protected abstract IReadOnlyList<TCollider> GetColliders(LayerMask mask);

        protected virtual void UpdateInteractionTargets()
        {
            if (Caster == null)
                return;

            _foundTargets.Clear();
            var colliders = GetColliders(InteractionMask);

            foreach (var col in colliders)
            {
                if (col == null)
                    continue;

                if (!col.TryGetComponent<IInteractable>(out var interactable))
                    continue;

                if (!interactable.CanInteract(gameObject))
                    continue;

                _foundTargets.Add(interactable);

                if (!InternalTargetsInRange.Contains(interactable))
                    AddTarget(interactable);
            }

            for (int i = InternalTargetsInRange.Count - 1; i >= 0; i--)
            {
                if (!_foundTargets.Contains(InternalTargetsInRange[i]))
                    RemoveTarget(InternalTargetsInRange[i]);
            }

            SelectBestTarget();
        }

        protected virtual void SelectBestTarget()
        {
            IInteractable best = null;
            float bestDist = float.MaxValue;

            if (Origin == null)
            {
                SetCurrentTarget(null);
                return;
            }

            Vector3 originPos = Origin.position;

            foreach (var target in InternalTargetsInRange)
            {
                if (target is not MonoBehaviour mb || mb == null)
                    continue;

                float dist = Vector3.Distance(originPos, mb.transform.position);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    best = target;
                }
            }

            SetCurrentTarget(best);
        }

        public override void Interact()
        {
            CurrentTarget?.Interact(gameObject);
            InvokeInteracted(CurrentTarget);
        }
    }
}