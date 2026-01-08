using Abstractions.Components.View;
using UnityEngine;

namespace Gameplay.Components.Health.View
{
    public class IntHealthTextView : BaseHealthTextView<int>
    {
        [SerializeField] protected IntHealthComponent Health;

        protected override void SubscribeToHealthEvents()
        {
            Health.OnHealthChanged += UpdateHealthView;
        }

        protected override void UnsubscribeToHealthEvents()
        {
            Health.OnHealthChanged -= UpdateHealthView;
        }

        protected override void InitializeHealthView()
        {
            UpdateHealthView(Health.CurrentHealth, Health.MaxHealth);
        }
    }
}