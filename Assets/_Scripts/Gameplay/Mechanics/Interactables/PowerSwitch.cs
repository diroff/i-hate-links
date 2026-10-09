using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Mechanics.Interactables
{
    public class PowerSwitch : MonoBehaviour
    {
        [SerializeField] private bool _isInitiallyOn = true;
        [SerializeField] private UnityEvent<bool> _onStateChangedUnityEvent;

        public event Action<bool> OnStateChanged;

        public bool IsOn { get; private set; }

        private void Awake()
        {
            IsOn = _isInitiallyOn;
        }

        public void Toggle()
        {
            SetState(!IsOn);
        }

        private void SetState(bool state)
        {
            if (IsOn == state)
                return;

            IsOn = state;

            OnStateChanged?.Invoke(IsOn);
            _onStateChangedUnityEvent?.Invoke(IsOn);
        }
    }
}