using Gameplay.Components.MiniGames;
using Reflex.Attributes;
using Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Gameplay.Mechanics.MiniGames
{
    [RequireComponent(typeof(ButtonMashQTE))]
    public class GrateMiniGame : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ButtonMashQTE _qteComponent;

        [Header("Events")]
        [SerializeField] private UnityEvent _onMiniGameStarted;
        [SerializeField] private UnityEvent _onMiniGameCompleted;

        [Inject] private InputService _input;

        public ButtonMashQTE QteComponent => _qteComponent;

        private void Awake()
        {
            if (_qteComponent == null)
                _qteComponent = GetComponent<ButtonMashQTE>();
        }

        private void OnEnable()
        {
            _qteComponent.OnCompleted += HandleQTECompleted;
        }

        private void OnDisable()
        {
            _qteComponent.OnCompleted -= HandleQTECompleted;
            UnsubscribeInput();
        }

        public void StartMiniGame()
        {
            if (_qteComponent.IsActive)
                return;

            _input.EnableMiniGames();
            SubscribeInput();

            _qteComponent.StartQTE();
            _onMiniGameStarted?.Invoke();
        }

        private void SubscribeInput()
        {
            if (_input == null) 
                return;

            _input.MiniGameInteractAction.started += OnMiniGameInteract;
        }

        private void UnsubscribeInput()
        {
            if (_input != null && _input.MiniGameInteractAction != null)
                _input.MiniGameInteractAction.started -= OnMiniGameInteract;
        }

        private void OnMiniGameInteract(InputAction.CallbackContext context)
        {
            if (!_qteComponent.IsActive)
                return;

            _qteComponent.RegisterClick();
        }

        private void HandleQTECompleted()
        {
            UnsubscribeInput();
            _input.EnableGameplay();

            _onMiniGameCompleted?.Invoke();
        }
    }
}