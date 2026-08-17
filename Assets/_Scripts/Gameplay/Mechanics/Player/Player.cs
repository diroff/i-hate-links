using Abstractions.Components;
using UnityEngine;

namespace Gameplay.Mechanics.Player
{
    public class Player : MonoBehaviour
    {
        private HealthComponent _health;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            _health.OnDied += HandleDeath;
            _health.OnRevived += HandleRevive;
        }

        private void OnDisable()
        {
            _health.OnDied -= HandleDeath;
            _health.OnRevived -= HandleRevive;
        }

        private void HandleDeath(GameObject dead, GameObject killer)
        {

        }

        private void HandleRevive(GameObject sender)
        {

        }
    }
}