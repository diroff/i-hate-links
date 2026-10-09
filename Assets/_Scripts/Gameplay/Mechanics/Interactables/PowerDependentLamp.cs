using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Gameplay.Mechanics.Interactables
{
    public class PowerDependentLamp : MonoBehaviour
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private SpriteRenderer _lightSprite;

        [Inject] private PowerSwitch _powerSwitch;

        private void OnEnable()
        {
            _powerSwitch.OnStateChanged += OnSwitchStateChanged;
            OnSwitchStateChanged(_powerSwitch.IsOn);
        }

        private void OnDisable()
        {
            _powerSwitch.OnStateChanged -= OnSwitchStateChanged;
        }

        private void OnSwitchStateChanged(bool isEnabled)
        {
            _light.enabled = isEnabled;
            _lightSprite.gameObject.SetActive(isEnabled);
        }
    }
}