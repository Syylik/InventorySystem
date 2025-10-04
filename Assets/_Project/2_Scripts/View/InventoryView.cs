using System;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventorySlot _slotPrefab;
        [SerializeField] private Transform _slotsParent;
        [SerializeField] private Transform _inventoryPanel;

        private InventorySlot[] _slots;

        public void Init(Inventory inventory)
        {
            if (inventory == null) throw new System.ArgumentNullException(nameof(Inventory));

            _inventory = inventory;
            _slots = new InventorySlot[_inventory.Items.Count];

            for (int i = 0; i < _slots.Length; i++)
            {
                var slot = Instantiate(_slotPrefab, _slotsParent);
                slot.Init(i, this, _inventoryPanel);
                _slots[i] = slot;
            }

            _inventory.OnSlotChanged += OnSlotChanged;
            RefreshAll();
        }

        private void OnSlotChanged(int index)
        {
            var item = _inventory.Items[index];
            if (item != null) _slots[index].SetItem(item);
            else _slots[index].Clear();
        }

        public void RefreshAll()
        {
            for (int i = 0; i < _slots.Length; i++) OnSlotChanged(i);
        }

        public void OnSwap(int from, int to) => _inventory.SwapItems(from, to);
        public void OnUse(int index) => _inventory.UseItem(index);
        public void OnDrop(int index) => _inventory.RemoveItem(index);
    }
}