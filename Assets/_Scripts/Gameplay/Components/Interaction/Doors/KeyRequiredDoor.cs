using Abstractions.Components.Inventory;
using Data;
using UnityEngine;

namespace Gameplay.Components.Interaction.Doors
{
    public class KeyRequiredDoor : BaseDoor
    {
        [Header("Key Requirements")]
        [SerializeField] private ItemDefinition _requiredKey;
        [SerializeField] private DialogueComponent _noKeyDialogue;

        protected override bool TryOpen(GameObject interactor)
        {
            if (_requiredKey == null)
                return true;

            if (interactor.TryGetComponent(out InventoryComponent inventory) && inventory.HasItem(_requiredKey))
            {
                inventory.Remove(_requiredKey, 1);
                return true;
            }

            OnKeyMissing(interactor);
            return false;
        }

        protected virtual void OnKeyMissing(GameObject interactor)
        {
            _noKeyDialogue.StartDialogue();
        }
    }
}