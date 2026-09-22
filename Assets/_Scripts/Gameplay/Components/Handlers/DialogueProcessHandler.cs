using Reflex.Attributes;
using Services;
using UnityEngine;

namespace Gameplay.Components.Handlers
{
    public class DialogueProcessHandler : MonoBehaviour
    {
        [Inject] private DialogueService _dialogueService;
        [Inject] private InputService _inputService;

        private void OnEnable()
        {
            _dialogueService.OnDialogueStarted += OnDialogueStarted;
            _dialogueService.OnDialogueEnded += OnDialogueEnded;
        }

        private void OnDisable()
        {
            _dialogueService.OnDialogueStarted -= OnDialogueStarted;
            _dialogueService.OnDialogueEnded -= OnDialogueEnded;
        }

        private void OnDialogueStarted()
        {
            _inputService.EnableUI();
        }

        private void OnDialogueEnded()
        {
            _inputService.EnableGameplay();
        }
    }
}