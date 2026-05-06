using Abstractions.Components;
using Abstractions.Components.Damage;
using Abstractions.Components.Zone;
using UnityEngine;

namespace Testers
{
    public class TestDamageArea : MonoBehaviour
    {
        [SerializeField] private Zone2DBase _zone;
        [SerializeField] private DamageComponent _damage;

        [SerializeField] private float _enterDamage = 2f;
        [SerializeField] private float _stayDamage = 0.1f;

        private void OnEnable()
        {
            _zone.OnEnter += OnTriggerZoneEnter;
            _zone.OnStay += OnTriggerZoneStay;
        }

        private void OnTriggerZoneEnter(Collider2D collider)
        {
            if (!collider.TryGetComponent(out HealthComponent health))
                return;

            _damage.DealDamage(health, _enterDamage);
        }

        private void OnTriggerZoneStay(Collider2D collider)
        {
            if (!collider.TryGetComponent(out HealthComponent health))
                return;

            _damage.DealDamage(health, _stayDamage);
        }
    }
}