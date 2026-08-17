using Abstractions.Components;
using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Mechanics.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private MovementPlane _plane = MovementPlane.XZ;

        [Inject] private InputService _input;

        private MovementComponent _movement;
        private JumpComponent _jump;
        private InteractionComponent _interaction;

        public bool IsInputBlocked { get; private set; }

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _jump = GetComponent<JumpComponent>();
            _interaction = GetComponent<InteractionComponent>();
        }

        private void Start()
        {
            _input.EnableGameplay(); //TODO: убрать, должно управляться в среде уровня
        }

        private void Update()
        {
            if (IsInputBlocked)
            {
                _movement.Move(Vector3.zero);
                return;
            }

            HandleMovement();
            HandleJump();
            HandleInteraction();
        }

        private void HandleMovement()
        {
            Vector2 rawInput = _input.MoveDirection;
            Vector3 direction = _plane switch
            {
                MovementPlane.XZ => new Vector3(rawInput.x, 0f, rawInput.y),
                MovementPlane.XY => new Vector3(rawInput.x, rawInput.y, 0f),
                MovementPlane.XZ_FlipY => new Vector3(rawInput.x, 0f, rawInput.y),
                _ => new Vector3(rawInput.x, 0f, rawInput.y)
            };

            _movement.Move(direction);
        }

        private void HandleJump()
        {
            if (_input.JumpAction.WasPressedThisFrame())
                _jump.OnJumpPressed();

            if (_input.JumpAction.WasReleasedThisFrame())
                _jump.OnJumpReleased();
        }

        private void HandleInteraction()
        {
            if (_input.InteractAction.WasPressedThisFrame())
                _interaction.Interact();
        }
    }
}