using Abstractions.Components.Inventory;
using Data;
using Gameplay.Components.Interaction;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay.Components.Inventory
{
    public class WorldItem : InteractableComponent
    {
        [Header("Item Settings")]
        [SerializeField] protected ItemDefinition Item;
        [SerializeField] protected int Amount = 1;

        public ItemDefinition ItemDefinition => Item;

        public override LocalizedString InteractionName => Item != null ? Item.Name : base.InteractionName;

        public override bool CanInteract(GameObject interactor)
        {
            if (!base.CanInteract(interactor))
                return false;

            return Item != null;
        }

        protected override void OnInteractInternal(GameObject interactor)
        {
            if (!interactor.TryGetComponent(out InventoryComponent inventory))
                return;

            AddItemToInventory(inventory);
        }

        protected virtual void AddItemToInventory(InventoryComponent inventory)
        {
            ProccessItemAdding(inventory);
        }

        protected virtual void ProccessItemAdding(InventoryComponent inventory)
        {
            inventory.Add(Item, Amount);
            OnItemAdded();
        }

        protected virtual void OnItemAdded()
        {
            Destroy(gameObject);
        }
    }
}