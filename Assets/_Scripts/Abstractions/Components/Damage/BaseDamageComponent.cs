using Abstractions.Interfaces;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Abstractions.Components.Damage
{
    public abstract class BaseDamageComponent<T> : MonoBehaviour
    {
        [SerializeField] protected bool CanDamageByStart = true;

        protected bool IsDamageEnabled;

        public UnityAction<IDamageable<T>, T> OnDealDamage;

        protected virtual void Start()
        {
            if (CanDamageByStart)
                EnableDamage();
        }

        public void EnableDamage()
        {
            IsDamageEnabled = true;
        }

        public void DisableDamage()
        {
            IsDamageEnabled = false;
        }

        public void DealDamage(IDamageable<T> damageable)
        {
            var damageValue = CalculateCurrentDamageValue();

            damageable.Damage(damageValue, gameObject);
            OnDealDamage?.Invoke(damageable, damageValue);
        }

        protected virtual bool CanDamage(GameObject obj)
        {
            return IsDamageEnabled;
        }

        public abstract T CalculateCurrentDamageValue();
    }
}