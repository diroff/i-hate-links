using Abstractions.Components;
using Abstractions.Interfaces;
using UnityEngine;

namespace Gameplay.Components.Interaction
{
    public class InteractionOutlineView : MonoBehaviour
    {
        [SerializeField] private InteractionComponent _interaction;

        private const string OutlineKeyword = "_OUTLINE_ON";

        private Renderer _currentRenderer;

        private void OnEnable()
        {
            _interaction.OnTargetChanged += OnTargetChanged;
        }

        private void OnDisable()
        {
            _interaction.OnTargetChanged -= OnTargetChanged;
            DisableOutline();
        }

        private void OnTargetChanged(IInteractable interactable)
        {
            DisableOutline();

            if (interactable is not Component targetComponent)
                return;

            if (!targetComponent.TryGetComponent(out _currentRenderer))
            {
                _currentRenderer = targetComponent.GetComponentInChildren<Renderer>();
            }

            if (_currentRenderer == null)
                return;

            EnableOutline();
        }

        private void EnableOutline()
        {
            if (_currentRenderer == null)
                return;

            _currentRenderer.material.EnableKeyword(OutlineKeyword);
        }

        private void DisableOutline()
        {
            if (_currentRenderer == null)
                return;

            _currentRenderer.material.DisableKeyword(OutlineKeyword);
            _currentRenderer = null;
        }
    }
}