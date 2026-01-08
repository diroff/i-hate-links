using Abstractions.Components;
using Gameplay.Components.Health;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Testers
{
    public class InputTester : MonoBehaviour
    {
        [SerializeField] private MovementPlane _plane;

        [Inject] private InputService _input;

        private MovementComponent _movement;
        private InteractionComponent _interaction;
        private JumpComponent _jump;
        private IntHealthComponent _health;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _interaction = GetComponent<InteractionComponent>();
            _jump = GetComponent<JumpComponent>();
            _health = GetComponent<IntHealthComponent>();
        }

        private void OnEnable()
        {
            _health.OnDied += OnDied;
            _health.OnRevived += OnRevive;
        }

        private void OnDisable()
        {
            _health.OnDied -= OnDied;
            _health.OnRevived -= OnRevive;
        }

        private void Start()
        {
            _input.EnableGameplay();
        }

        private void Update()
        {
            var input2D = _input.Move.ReadValue<Vector2>();

            Vector3 direction = _plane switch
            {
                MovementPlane.XZ => new Vector3(input2D.x, 0f, input2D.y),
                MovementPlane.XY => new Vector3(input2D.x, input2D.y, 0f),
                MovementPlane.XZ_FlipY => new Vector3(input2D.x, 0f, input2D.y),
                _ => new Vector3(input2D.x, 0f, input2D.y)
            };

            _movement?.Move(direction);

            if (_input.Jump.WasPressedThisFrame())
                _jump?.OnJumpPressed();

            if (_input.Jump.WasReleasedThisFrame())
                _jump?.OnJumpReleased();


            if (_input.Attack.WasPressedThisFrame()) StartAttack();

            if (_input.Interact.WasPressedThisFrame())
            {
                _interaction?.Interact();
                _health.Revive(gameObject);
            }

            if (_input.Crouch.WasPressedThisFrame()) Crouch();
        }

        private void OnDied(GameObject dead, GameObject killer)
        {
            _movement.DisableMoving();
            _jump.DisableJumping();
        }

        private void OnRevive(GameObject sender)
        {
            _movement.EnableMoving();
            _jump.EnableJumping();
        }

        private void StartAttack()
        {
            _health.Damage(2, gameObject);
        }

        private void Crouch()
        {
            _health.ChangeMaxValue(_health.MaxHealth + 2, gameObject);
        }
    }
}