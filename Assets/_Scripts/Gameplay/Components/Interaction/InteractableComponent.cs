using System;
using Abstractions.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace Gameplay.Components.Interaction
{
    public class InteractableComponent : MonoBehaviour, IInteractable, IInteractableData
    {
        [Header("Settings")]
        [SerializeField] private bool _isInteractable = true;
        [SerializeField] private bool _oneShot = false;

        [Header("Data")]
        [SerializeField] private LocalizedString _interactionEntry;

        [Header("Events")]
        [SerializeField] private UnityEvent<GameObject> _onInteract;

        private bool _hasInteracted;

        public event Action<GameObject> OnInteracted;
        public event Action<bool> OnInteractableStateChanged;

        public virtual LocalizedString InteractionName => _interactionEntry;

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

        public void Interact(GameObject interactor)
        {
            if (!CanInteract(interactor))
                return;

            if (_oneShot)
                _hasInteracted = true;

            OnInteractInternal(interactor);
            OnInteracted?.Invoke(interactor);
            _onInteract?.Invoke(interactor);
        }

        public virtual bool CanInteract(GameObject interactor)
        {
            if (!_isInteractable)
                return false;

            if (_oneShot && _hasInteracted)
                return false;

            return true;
        }

        protected virtual void OnInteractInternal(GameObject interactor)
        {
        }
    }
}