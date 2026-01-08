using Abstractions.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Abstractions.Components
{
    public abstract class BaseHealthComponent<T> : MonoBehaviour, IDamageable<T>
    {
        public T CurrentHealth { get; protected set; }
        public T MaxHealth { get; protected set; }

        public bool IsDead { get; protected set; }

        public UnityAction<T, T> OnHealthChanged;
        public UnityAction<GameObject> OnDamage;
        public UnityAction<GameObject> OnHeal;
        public UnityAction<GameObject, GameObject> OnDied;
        public UnityAction<GameObject> OnRevived;

        protected virtual void Awake()
        {
            Initialize();
        }

        protected abstract void Initialize();

        public abstract void Damage(T value, GameObject sender);

        public abstract void Heal(T value, GameObject sender);

        public abstract void ChangeMaxValue(T newValue, GameObject sender);

        public virtual void Revive(GameObject sender)
        {
            if (!IsDead)
                return;

            IsDead = false;
            Heal(MaxHealth, sender);

            OnRevived?.Invoke(sender);
        }

        protected virtual void Die(GameObject killer)
        {
            if (IsDead)
                return;

            IsDead = true;

            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            OnDied?.Invoke(gameObject, killer);
        }
    }
}