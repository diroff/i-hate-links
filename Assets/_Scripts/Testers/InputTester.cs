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

        private void Awake()
        {
            _movement = GetComponent<MovementComponent>();
            _interaction = GetComponent<InteractionComponent>();
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

            if (_input.Jump.WasPressedThisFrame()) Debug.Log("Jump press");
            if (_input.Jump.IsPressed()) Debug.Log("Jump hold");
            if (_input.Jump.WasReleasedThisFrame()) Debug.Log("Jump release");

            if (_input.Attack.WasPressedThisFrame()) StartAttack();

            if (_input.Interact.WasPressedThisFrame())
                _interaction?.Interact();

            if (_input.Crouch.WasPressedThisFrame()) Crouch();
        }

        private void StartAttack() => Debug.Log("Attack");
        private void Crouch() => Debug.Log("Crouch");
    }
}