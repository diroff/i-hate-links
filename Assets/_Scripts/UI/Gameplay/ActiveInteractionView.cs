using Abstractions.Components;
using Abstractions.Interfaces;
using Gameplay.Components.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Testers
{
    public class ActiveInteractionView : MonoBehaviour
    {
        [SerializeField] private InteractionComponent _interaction;
        [SerializeField] private TMP_Text _interactionText;

        private void OnEnable()
        {
            ClearSelectedName();

            _interaction.OnTargetChanged += OnTargetChanged;
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
        }

        private void OnDisable()
        {
            _interaction.OnTargetChanged -= OnTargetChanged;
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }

        private void OnTargetChanged(IInteractable interactable)
        {
            if (interactable == null)
            {
                ClearSelectedName();
                return;
            }

            if (interactable is not WorldItem)
            {
                ClearSelectedName();
                return;
            }

            var item = interactable as WorldItem;
            var itemId = item.ItemDefinition.Id;

            UpdateSelectedItem(itemId);
        }

        private void UpdateSelectedItem(string key)
        {
            _interactionText.text = LocalizationSettings.StringDatabase.GetLocalizedString("Items", key);
        }

        private void OnSelectedLocaleChanged(Locale locale)
        {
            var interactable = _interaction.CurrentTarget;

            if (interactable == null)
            {
                ClearSelectedName();
                return;
            }

            OnTargetChanged(interactable);
        }

        private void ClearSelectedName()
        {
            _interactionText.text = "";
        }
    }
}