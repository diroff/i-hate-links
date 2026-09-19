using Abstractions.Components.View;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Components.View
{
    public class HealthSliderView : BaseHealthView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationSpeed = 100f;

        private Tween _valueTween;


        protected virtual void Awake()
        {
            if (_slider == null)
                _slider = GetComponentInChildren<Slider>();
        }

        protected override void InitializeHealthView()
        {
            _slider.maxValue = Health.MaxHealth;
            _slider.value = Health.CurrentHealth;
        }

        protected override void UpdateHealthView(float currentValue, float maxValue)
        {
            _slider.maxValue = maxValue;

            float duration = Mathf.Abs(_slider.value - currentValue) / _animationSpeed;

            _valueTween?.Kill();

            _valueTween = _slider
                .DOValue(currentValue, duration)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }
    }
}