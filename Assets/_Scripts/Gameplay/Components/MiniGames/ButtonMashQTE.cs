using System;
using UnityEngine;

namespace Gameplay.Components.MiniGames
{
    public class ButtonMashQTE : MonoBehaviour
    {
        [Header("QTE Settings")]
        [SerializeField] private float _fillPerClick = 0.1f;
        [SerializeField] private float _decayRate = 0.15f;
        [SerializeField] private bool _autoResetOnFail = false;

        public event Action<float> OnProgressChanged;
        public event Action OnCompleted;
        public event Action OnFailed;

        public float Progress { get; private set; }
        public bool IsActive { get; private set; }

        public void StartQTE()
        {
            Progress = 0f;
            IsActive = true;
            OnProgressChanged?.Invoke(Progress);
        }

        public void StopQTE()
        {
            IsActive = false;
        }

        public void RegisterClick()
        {
            if (!IsActive)
                return;

            Progress = Mathf.Clamp01(Progress + _fillPerClick);
            OnProgressChanged?.Invoke(Progress);

            if (Mathf.Approximately(Progress, 1f))
            {
                IsActive = false;
                OnCompleted?.Invoke();
            }
        }

        private void Update()
        {
            if (!IsActive || Progress <= 0f)
                return;

            Progress = Mathf.Clamp01(Progress - _decayRate * Time.deltaTime);
            OnProgressChanged?.Invoke(Progress);

            if (Progress <= 0f && _autoResetOnFail)
            {
                OnFailed?.Invoke();
            }
        }
    }
}