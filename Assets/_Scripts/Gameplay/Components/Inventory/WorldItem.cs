using Abstractions.Components.Inventory;
using Abstractions.Interfaces;
using Data;
using UnityEngine;

namespace Gameplay.Components.Inventory
{
    public class WorldItem : MonoBehaviour, IInteractable
    {
        [SerializeField] protected ItemDefinition Item;
        [SerializeField] protected int Amount = 1;

        public void Interact(GameObject interactor)
        {
            if (Item == null)
                return;

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
            Debug.Log("Adding...");

            inventory.Add(Item, Amount);
            OnItemAdded();
            Debug.Log("Destroyed");
        }

        protected virtual void OnItemAdded()
        {
            Destroy(gameObject);
        }

        public bool CanInteract(GameObject interactor)
        {
            return Item != null;
        }
    }
}