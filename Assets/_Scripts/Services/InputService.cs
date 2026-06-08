using Abstractions.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.InputSystem;

namespace Services
{
    public class InputService : IService, IDisposable
    {
        private InputSystem_Actions _input;

        public InputAction Move => _input.Player.Move;
        public InputAction Jump => _input.Player.Jump;
        public InputAction Interact => _input.Player.Interact;

        public InputSystem_Actions Actions => _input;

        public InputService()
        {
            _input = new InputSystem_Actions();
        }

        public UniTask Initialize()
        {
            _input.Enable();
            return UniTask.CompletedTask;
        }

        public void Dispose()
        {
            _input?.Disable();
            _input?.Dispose();
        }

        public void EnableGameplay() => _input.Player.Enable();
        public void DisableGameplay() => _input.Player.Disable();
        public void EnableUI() => _input.UI.Enable();
        public void DisableUI() => _input.UI.Disable();
    }
}