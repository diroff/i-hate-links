using System;
using Abstractions.Interfaces;
using UnityEngine;

namespace Gameplay.Components.Interaction.Doors
{
    public class BaseDoor : InteractableComponent, ILevelCompleter
    {
        public event Action<ILevelCompleter> OnLevelCompleted;
        public event Action OnOpeningStarted;
        public event Action OnOpened;

        public bool IsOpen { get; private set; }
        public bool IsOpening { get; private set; }
        public bool IsCompleted => IsOpen;

        protected override void OnInteractInternal(GameObject interactor)
        {
            if (IsOpen || IsOpening)
                return;

            if (TryOpen(interactor))
            {
                StartOpening();
            }
        }

        protected virtual bool TryOpen(GameObject interactor)
        {
            return true;
        }

        public void StartOpening()
        {
            if (IsOpen || IsOpening)
                return;

            IsOpening = true;
            IsInteractable = false;
            OnOpeningStarted?.Invoke();
        }

        public void SetOpened()
        {
            if (IsOpen)
                return;

            IsOpening = false;
            IsOpen = true;

            OnOpened?.Invoke();
            OnLevelCompleted?.Invoke(this);
        }
    }
}