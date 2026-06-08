using Abstractions.Components;
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
        private HealthComponent _health;
        private Animator _animator;

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _interaction = GetComponent<InteractionComponent>();
            _jump = GetComponent<JumpComponent>();
            _health = GetComponent<HealthComponent>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _health.OnDied += OnDied;
            _health.OnRevived += OnRevive;
            _jump.JumpStarted += OnJumpStarted;
            _jump.Landed += OnLanded;
            _movement.OnDirectionChanged += OnDirectionChanged;
            _movement.OnMoveStarted += OnMoveStarted;
            _movement.OnMoveStopped += OnMoveStopped;
        }

        private void OnDisable()
        {
            _health.OnDied -= OnDied;
            _health.OnRevived -= OnRevive;
            _jump.JumpStarted -= OnJumpStarted;
            _jump.Landed -= OnLanded;
            _movement.OnDirectionChanged -= OnDirectionChanged;
            _movement.OnMoveStarted -= OnMoveStarted;
            _movement.OnMoveStopped -= OnMoveStopped;
        }

        private void Start()
        {
            _input.EnableGameplay();
        }

        private void OnMoveStarted(Vector3 direction)
        {
            _animator.SetFloat("Speed", 1f);
        }

        private void OnMoveStopped()
        {
            _animator.SetFloat("Speed", 0f);
        }

        private void OnJumpStarted()
        {
            _animator.SetBool("IsJump", true);
        }

        private void OnLanded()
        {
            _animator.SetBool("IsJump", false);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            if (direction.x == 0)
                return;

            if (direction.x > 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
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


            if (_input.Interact.WasPressedThisFrame())
            {
                _interaction?.Interact();
                _animator.SetTrigger("Interact");
                _health.Revive(gameObject);
            }
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
    }
}