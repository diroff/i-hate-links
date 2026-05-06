using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abstractions.Components.Zone
{
    public abstract class ZoneBase<TCollider> : MonoBehaviour where TCollider : Component
    {
        [SerializeField] protected LayerMask LayerMask = ~0;
        [SerializeField] protected float StayFrequency = 0f;

        public event Action<TCollider> OnEnter;
        public event Action<TCollider> OnExit;
        public event Action<TCollider> OnStay;

        protected readonly Dictionary<TCollider, float> StayTimers = new();

        protected void HandleEnter(TCollider other)
        {
            if (!IsValid(other))
                return;

            OnEnter?.Invoke(other);

            if (StayFrequency > 0f)
                StayTimers[other] = 0f;
        }

        protected void HandleExit(TCollider other)
        {
            if (!IsValid(other))
                return;

            OnExit?.Invoke(other);
            StayTimers.Remove(other);
        }

        protected void HandleStay(TCollider other)
        {
            if (!IsValid(other))
                return;

            if (StayFrequency <= 0f)
            {
                OnStay?.Invoke(other);
                return;
            }

            if (!StayTimers.ContainsKey(other))
                StayTimers[other] = 0f;

            StayTimers[other] += Time.deltaTime;

            if (StayTimers[other] >= 1f / StayFrequency)
            {
                StayTimers[other] = 0f;
                OnStay?.Invoke(other);
            }
        }

        protected virtual bool IsValid(TCollider other)
        {
            return (LayerMask.value & (1 << other.gameObject.layer)) != 0;
        }
    }
}