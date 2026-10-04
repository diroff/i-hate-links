using System;
using Abstractions.Components.Inventory;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Components.Interaction
{
    public class LockedInteractableComponent : InteractableComponent
    {
        [Header("Locked Settings")]
        [SerializeField] private bool _isLocked = true;

        [Header("Key Requirement (Optional)")]
        [SerializeField] private ItemDefinition _requiredKey;
        [SerializeField] private bool _consumeKeyOnUnlock = true;

        [Header("Locked Events")]
        [SerializeField] private UnityEvent<GameObject> _onLockedInteract;
        [SerializeField] private UnityEvent _onUnlocked;
        [SerializeField] private UnityEvent _onLocked;

        public event Action OnUnlockedEvent;
        public event Action OnLockedEvent;

        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                if (_isLocked == value)
                    return;

                if (value)
                    Lock();
                else
                    Unlock();
            }
        }

        public ItemDefinition RequiredKey => _requiredKey;

        public override bool CanInteract(GameObject interactor)
        {
            if (!IsInteractable)
                return false;

            if (_isLocked)
                return true;

            return base.CanInteract(interactor);
        }

        public override void Interact(GameObject interactor)
        {
            if (!CanInteract(interactor))
                return;

            if (_isLocked)
                HandleLockedInteract(interactor);
            else
                HandleUnlockedInteract(interactor);
        }

        private void HandleLockedInteract(GameObject interactor)
        {
            if (TryUnlock(interactor))
                ProcessSuccessfulUnlock(interactor);
            else
                ProcessFailedUnlock(interactor);
        }

        private void HandleUnlockedInteract(GameObject interactor)
        {

            base.Interact(interactor);
        }

        private void ProcessSuccessfulUnlock(GameObject interactor)
        {
            Unlock();
            HandleUnlockedInteract(interactor);
        }

        private void ProcessFailedUnlock(GameObject interactor)
        {
            OnLockedInteractInternal(interactor);
            _onLockedInteract?.Invoke(interactor);
        }

        protected virtual bool TryUnlock(GameObject interactor)
        {
            if (_requiredKey == null)
                return false;

            if (!HasRequiredKey(interactor, out var inventory))
                return false;

            ConsumeKeyIfNeeded(inventory);
            return true;
        }

        private bool HasRequiredKey(GameObject interactor, out InventoryComponent inventory)
        {
            inventory = null;

            if (interactor == null)
                return false;

            return interactor.TryGetComponent(out inventory) && inventory.HasItem(_requiredKey);
        }

        private void ConsumeKeyIfNeeded(InventoryComponent inventory)
        {
            if (_consumeKeyOnUnlock)
                inventory.Remove(_requiredKey, 1);
        }

        protected virtual void OnLockedInteractInternal(GameObject interactor)
        {
        }

        public virtual void Unlock()
        {
            _isLocked = false;
            OnUnlockedInternal();
            OnUnlockedEvent?.Invoke();
            _onUnlocked?.Invoke();
        }

        public virtual void Lock()
        {
            _isLocked = true;
            OnLockedInternal();
            OnLockedEvent?.Invoke();
            _onLocked?.Invoke();
        }

        protected virtual void OnUnlockedInternal()
        {
        }

        protected virtual void OnLockedInternal()
        {
        }
    }
}