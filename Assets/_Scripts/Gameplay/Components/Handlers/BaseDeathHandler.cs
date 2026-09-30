using Abstractions.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Components.Handlers
{
    public class BaseDeathHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected HealthComponent _health;

        [Header("Events")]
        [SerializeField] private UnityEvent _onDied;
        [SerializeField] private UnityEvent _onRevived;

        protected virtual void OnEnable()
        {
            if (_health == null)
                _health = GetComponent<HealthComponent>();

            if (_health != null)
            {
                _health.OnDied += OnDiedHandler;
                _health.OnRevived += OnRevivedHandler;
            }
        }

        protected virtual void OnDisable()
        {
            if (_health != null)
            {
                _health.OnDied -= OnDiedHandler;
                _health.OnRevived -= OnRevivedHandler;
            }
        }

        private void OnDiedHandler(GameObject dead, GameObject killer)
        {
            HandleDeath(dead, killer);
            _onDied?.Invoke();
        }

        private void OnRevivedHandler(GameObject sender)
        {
            HandleRevive(sender);
            _onRevived?.Invoke();
        }

        protected virtual void HandleDeath(GameObject dead, GameObject killer)
        {
        }

        protected virtual void HandleRevive(GameObject sender)
        {
        }
    }
}