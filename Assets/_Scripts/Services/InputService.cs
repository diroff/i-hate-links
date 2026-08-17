using Abstractions.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services
{
    public class InputService : IService, IDisposable
    {
        private InputSystem_Actions _input;

        public Vector2 MoveDirection => _input.Player.Move.ReadValue<Vector2>();
        public InputAction JumpAction => _input.Player.Jump;
        public InputAction InteractAction => _input.Player.Interact;

        public InputSystem_Actions Actions => _input;

        public InputService()
        {
            _input = new InputSystem_Actions();
        }

        public UniTask Initialize()
        {
            EnableGameplay();
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _input?.Disable();
            _input?.Dispose();
        }

        public void EnableGameplay()
        {
            _input.UI.Disable();
            _input.Player.Enable();
        }

        public void EnableUI()
        {
            _input.Player.Disable();
            _input.UI.Enable();
        }

        public string GetBindingsJson() => _input.SaveBindingOverridesAsJson();
        public void ApplyBindingsJson(string json) => _input.LoadBindingOverridesFromJson(json);
        public void ResetBindings() => _input.RemoveAllBindingOverrides();
    }
}