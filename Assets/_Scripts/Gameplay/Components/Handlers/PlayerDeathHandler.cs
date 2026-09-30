using Abstractions.Components;
using Gameplay.Mechanics.Player;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Components.Handlers
{
    public class PlayerDeathHandler : BaseDeathHandler
    {
        [Inject] private Player _player;
        [Inject] private InputService _inputService;

        private Renderer[] _renderers;
        private Collider2D[] _colliders;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            if (_health == null && _player != null)
            {
                _health = _player.GetComponent<HealthComponent>();
            }
        }

        protected override void OnEnable()
        {
            if (_player != null)
            {
                _renderers = _player.GetComponentsInChildren<Renderer>(true);
                _colliders = _player.GetComponentsInChildren<Collider2D>(true);
                _rigidbody = _player.GetComponent<Rigidbody2D>();
            }

            base.OnEnable();
        }

        protected override void HandleDeath(GameObject dead, GameObject killer)
        {
            base.HandleDeath(dead, killer);

            _inputService?.EnableUI();

            if (_renderers != null)
            {
                foreach (var rnd in _renderers)
                    rnd.enabled = false;
            }

            if (_colliders != null)
            {
                foreach (var col in _colliders)
                    col.enabled = false;
            }

            if (_rigidbody != null)
            {
                _rigidbody.linearVelocity = Vector2.zero;
                _rigidbody.simulated = false;
            }
        }

        protected override void HandleRevive(GameObject sender)
        {
            base.HandleRevive(sender);

            _inputService?.EnableGameplay();

            if (_renderers != null)
            {
                foreach (var rnd in _renderers)
                    rnd.enabled = true;
            }

            if (_colliders != null)
            {
                foreach (var col in _colliders)
                    col.enabled = true;
            }

            if (_rigidbody != null)
            {
                _rigidbody.simulated = true;
            }
        }
    }
}