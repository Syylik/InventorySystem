using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _capacity = 30;
        public int Capacity => _capacity;
        private InventoryItem[] _items;
        public IReadOnlyList<InventoryItem> Items => _items;

        public event Action<int> OnSlotChanged;

        public void Init()
        {
            _items = new InventoryItem[_capacity];
        }

        public bool TryAddItem(ItemData itemData, int amount = 1)
        {
            if (amount <= 0) return false;

            // Add to existing slot
            if (itemData.isStackable)
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    if (_items[i] != null && _items[i].data == itemData)
                    {
                        _items[i].amount += amount;
                        OnSlotChanged?.Invoke(i);
                        return true;
                    }
                }
            }

            // Finds empty slot and inserts in it
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = new InventoryItem(itemData, amount);
                    OnSlotChanged?.Invoke(i);
                    return true;
                }
            }

            return false;
        }

        public void RemoveItem(int index, int amount)
        {
            if (_items[index] == null) return;
            _items[index].amount -= amount;
            if (_items[index].amount <= 0) _items[index] = null;
        }

        public void SwapItems(int fromIndex, int toIndex)
        {
            var temp = _items[fromIndex];
            _items[fromIndex] = _items[toIndex];
            _items[toIndex] = temp;

            OnSlotChanged?.Invoke(fromIndex);
            OnSlotChanged?.Invoke(toIndex);
        }

        public void UseItem(int index)
        {
            if (_items[index] == null) return;
            Debug.Log(_items[index].data.name + " Used");
            // _items[index].Use();
            RemoveItem(index, 1);
            OnSlotChanged?.Invoke(index);
        }
    }
}