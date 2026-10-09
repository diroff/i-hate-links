using UnityEngine;

namespace Gameplay.Mechanics.Interactables
{
    public class PowerSwitchView : MonoBehaviour
    {
        [SerializeField] private PowerSwitch _powerSwitch;

        [SerializeField] private SpriteRenderer _spriteRender;
        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private void OnEnable()
        {
            _powerSwitch.OnStateChanged += OnStateChanged;
            OnStateChanged(_powerSwitch.IsOn);
        }

        private void OnDisable()
        {
            _powerSwitch.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(bool isEnabled)
        {
            if (enabled)
                OnSwitchEnabled();
            else
                OnSwitchDisabled();
        }

        private void OnSwitchEnabled()
        {
            _spriteRender.sprite = _onSprite;
        }

        private void OnSwitchDisabled()
        {
            _spriteRender.sprite = _offSprite;
        }
    }
}