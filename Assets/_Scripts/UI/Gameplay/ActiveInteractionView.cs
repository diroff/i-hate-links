using Abstractions.Components;
using Abstractions.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay.UI
{
    public class ActiveInteractionView : MonoBehaviour
    {
        [SerializeField] private InteractionComponent _interaction;
        [SerializeField] private TMP_Text _interactionText;

        private LocalizedString _currentLocalizedString;

        private void OnEnable()
        {
            ClearText();
            _interaction.OnTargetChanged += OnTargetChanged;
        }

        private void OnDisable()
        {
            _interaction.OnTargetChanged -= OnTargetChanged;
            UnsubscribeCurrentString();
        }

        private void OnTargetChanged(IInteractable interactable)
        {
            UnsubscribeCurrentString();

            if (interactable is not IInteractableData dataHolder)
            {
                ClearText();
                return;
            }

            _currentLocalizedString = dataHolder.InteractionName;

            if (_currentLocalizedString == null || _currentLocalizedString.IsEmpty)
            {
                ClearText();
                return;
            }

            _currentLocalizedString.StringChanged += OnStringChanged;
            _currentLocalizedString.RefreshString();
        }

        private void OnStringChanged(string localizedValue)
        {
            _interactionText.text = localizedValue;
        }

        private void UnsubscribeCurrentString()
        {
            if (_currentLocalizedString != null)
            {
                _currentLocalizedString.StringChanged -= OnStringChanged;
                _currentLocalizedString = null;
            }
        }

        private void ClearText()
        {
            _interactionText.text = string.Empty;
        }
    }
}