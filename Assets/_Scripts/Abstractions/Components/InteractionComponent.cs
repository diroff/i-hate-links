using Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Components
{
    public abstract class InteractionComponent : MonoBehaviour
    {
        [SerializeField] protected float InteractionRange = 2f;
        [SerializeField] protected LayerMask InteractionMask;
        [SerializeField] protected QueryTriggerInteraction TriggerMode = QueryTriggerInteraction.Collide;

        public IInteractable CurrentTarget { get; protected set; }

        public IReadOnlyList<IInteractable> TargetsInRange => _targetsInRange;
        private readonly List<IInteractable> _targetsInRange = new();

        public event Action<IInteractable> OnTargetEnter;
        public event Action<IInteractable> OnTargetExit;
        public event Action<IInteractable> OnTargetChanged;
        public event Action<IInteractable> OnInteracted;

        protected virtual void Update()
        {
            UpdateInteractionTargets();
        }

        protected abstract void UpdateInteractionTargets();

        protected void SetCurrentTarget(IInteractable newTarget)
        {
            if (CurrentTarget == newTarget)
                return;

            CurrentTarget = newTarget;
            OnTargetChanged?.Invoke(CurrentTarget);
        }

        protected void AddTarget(IInteractable target)
        {
            if (_targetsInRange.Contains(target))
                return;

            _targetsInRange.Add(target);
            OnTargetEnter?.Invoke(target);
        }

        protected void RemoveTarget(IInteractable target)
        {
            if (!_targetsInRange.Remove(target))
                return;

            OnTargetExit?.Invoke(target);

            if (CurrentTarget == target)
                SetCurrentTarget(null);
        }

        public void Interact()
        {
            CurrentTarget?.Interact(gameObject);
            OnInteracted?.Invoke(CurrentTarget);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = CurrentTarget != null ? Color.green : Color.yellow;
            DrawInteractionGizmo();
        }

        protected abstract void DrawInteractionGizmo();
    }
}