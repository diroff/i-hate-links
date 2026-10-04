using Data.Dialogue;
using Reflex.Attributes;
using Services;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Components
{
    public class DialogueComponent : MonoBehaviour
    {
        [SerializeField] private DialogueSO _dialogue;

        [SerializeField] private bool _isInteractable = true;
        [SerializeField] private bool _oneShot = false;

        [Header("Events")]
        [SerializeField] private UnityEvent _onDialogueEnded;

        [Inject] private DialogueService _dialogueService;

        public event Action OnInteracted;
        public event Action<bool> OnInteractableStateChanged;

        private bool _hasInteracted;

        public DialogueSO Dialogue => _dialogue;

        public bool IsInteractable
        {
            get => _isInteractable;
            set
            {
                if (_isInteractable == value)
                    return;

                _isInteractable = value;
                OnInteractableStateChanged?.Invoke(_isInteractable);
            }
        }

        private void OnEnable()
        {
            if (_dialogueService != null)
                _dialogueService.OnDialogueEndedWithSO += HandleDialogueEnded;
        }

        private void OnDisable()
        {
            if (_dialogueService != null)
                _dialogueService.OnDialogueEndedWithSO -= HandleDialogueEnded;
        }

        public void StartDialogue()
        {
            if (!CanStart())
                return;

            if (_dialogue == null || _dialogueService == null)
                return;

            if (_oneShot)
                _hasInteracted = true;

            _dialogueService.StartDialogue(_dialogue);
            OnInteracted?.Invoke();
        }

        public bool CanStart()
        {
            if (!_isInteractable)
                return false;

            if (_oneShot && _hasInteracted)
                return false;

            return true;
        }

        private void HandleDialogueEnded(DialogueSO endedDialogue)
        {
            if (endedDialogue == _dialogue)
                _onDialogueEnded?.Invoke();
        }
    }
}