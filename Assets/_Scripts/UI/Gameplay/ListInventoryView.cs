using Abstractions.Components.Inventory;
using Abstractions.UI.Inventory;
using Data;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Gameplay
{
    public class ListInventoryView : InventoryView
    {
        [SerializeField] private InventoryComponent _inventory;
        [SerializeField] private Transform _container;
        [SerializeField] private InventorySlotView _prefab;

        private readonly List<InventorySlotView> _slots = new();

        private void Awake()
        {
            Bind(_inventory);
        }

        protected override void SyncFull()
        {
            RefreshSlots();
        }

        protected override void SyncDiff()
        {
            RefreshSlots();
        }

        private void RefreshSlots()
        {
            var items = Inventory.GetItems();

            EnsureSlots(items.Count);

            for (int i = 0; i < items.Count; i++)
            {
                _slots[i].gameObject.SetActive(true);
                _slots[i].Set(items[i]);
            }

            for (int i = items.Count; i < _slots.Count; i++)
            {
                _slots[i].gameObject.SetActive(false);
            }
        }

        private void EnsureSlots(int count)
        {
            while (_slots.Count < count)
            {
                var slot = Instantiate(_prefab, _container);
                _slots.Add(slot);
            }
        }
    }
}