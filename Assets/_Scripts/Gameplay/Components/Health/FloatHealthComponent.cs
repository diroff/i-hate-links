using Abstractions.Components;
using UnityEngine;


namespace Gameplay.Components.Health
{
    public class FloatHealthComponent : BaseHealthComponent<float>
    {
        [SerializeField] protected float BaseHealth;

        protected override void Initialize()
        {
            MaxHealth = BaseHealth;
            CurrentHealth = MaxHealth;

            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

            IsDead = false;
        }

        public override void Damage(float value, GameObject sender)
        {
            if (IsDead)
                return;

            if (value <= 0)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth - value, 0, MaxHealth);

            if (CurrentHealth <= 0)
                Die(sender);

            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            OnDamage?.Invoke(sender);
        }

        public override void Heal(float value, GameObject sender)
        {
            if (IsDead)
                return;

            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            OnHeal?.Invoke(sender);
        }

        public override void ChangeMaxValue(float newValue, GameObject sender)
        {
            if (newValue <= 0)
                return;

            MaxHealth = newValue;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }
    }
}