using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Testers
{
    public class InputTester : MonoBehaviour
    {
        [Inject] private InputService _input;

        private void Start()
        {
            _input.EnableGameplay();
        }

        private void Update()
        {
            var move = _input.Move.ReadValue<Vector2>();
            transform.Translate(new Vector3(move.x, move.y, 0f) * 5f * Time.deltaTime);

            if (_input.Jump.WasPressedThisFrame()) Debug.Log("Jump press");
            if (_input.Jump.IsPressed()) Debug.Log("Jump hold");
            if (_input.Jump.WasReleasedThisFrame()) Debug.Log("Jump release");

            if (_input.Attack.WasPressedThisFrame()) StartAttack();
            if (_input.Interact.WasPressedThisFrame()) Interact();
            if(_input.Crouch.WasPressedThisFrame()) Crouch();
        }

        private void StartAttack() => Debug.Log("Attack");
        private void Interact() => Debug.Log("Interact");
        private void Crouch() => Debug.Log("Crouch");
    }
}