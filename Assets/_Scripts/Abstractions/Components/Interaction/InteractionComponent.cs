using System;
using System.Collections.Generic;
using Abstractions.Interfaces;
using UnityEngine;

namespace Abstractions.Components
{
    public abstract class InteractionComponent : MonoBehaviour
    {
        public IInteractable CurrentTarget { get; protected set; }
        public IReadOnlyList<IInteractable> TargetsInRange => InternalTargetsInRange;

        protected readonly List<IInteractable> InternalTargetsInRange = new();

        public event Action<IInteractable> OnTargetEnter;
        public event Action<IInteractable> OnTargetExit;
        public event Action<IInteractable> OnTargetChanged;
        public event Action<IInteractable> OnInteracted;

        public abstract void Interact();

        protected void SetCurrentTarget(IInteractable newTarget)
        {
            if (CurrentTarget == newTarget)
                return;

            CurrentTarget = newTarget;
            OnTargetChanged?.Invoke(CurrentTarget);
        }

        protected void AddTarget(IInteractable target)
        {
            if (InternalTargetsInRange.Contains(target))
                return;

            InternalTargetsInRange.Add(target);
            OnTargetEnter?.Invoke(target);
        }

        protected void RemoveTarget(IInteractable target)
        {
            if (!InternalTargetsInRange.Remove(target))
                return;

            OnTargetExit?.Invoke(target);

            if (CurrentTarget == target)
                SetCurrentTarget(null);
        }

        protected void InvokeInteracted(IInteractable target)
        {
            OnInteracted?.Invoke(target);
        }
    }
}