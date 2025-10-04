using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class Inventory
    {
        private int _capacity = 30;
        public int Capacity => _capacity;
        private InventoryItem[] _items;
        public IReadOnlyList<InventoryItem> Items => _items;

        public event Action<int> OnSlotChanged;

        public Inventory(int capacity = 30)
        {
            _capacity = capacity;
            _items = new InventoryItem[_capacity];
        }

        public Inventory(InventoryItem[] items)
        {
            _items = new InventoryItem[items.Length];
            items.CopyTo(_items, 0);
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
        public bool TryAddItem(ItemData itemData, int index, int amount = 1)
        {
            if (amount <= 0) return false;
            if (_items[index] != null)
            {
                if (itemData.isStackable)
                {
                    _items[index].amount += amount;
                    OnSlotChanged?.Invoke(index);
                    return true;
                }
                else return false;
            }

            _items[index] = new InventoryItem(itemData, amount);
            OnSlotChanged?.Invoke(index);
            return true;
        }

        public void RemoveItem(int index, int amount = 1)
        {
            if (_items[index] == null) return;
            _items[index].amount -= amount;
            if (_items[index].amount <= 0) _items[index] = null;

            OnSlotChanged?.Invoke(index);
        }

        public void SwapItems(int fromIndex, int toIndex)
        {
            if (_items[toIndex] != null && _items[fromIndex].data == _items[toIndex].data && _items[fromIndex].data.isStackable)
            {
                _items[toIndex].amount += _items[fromIndex].amount;
                _items[fromIndex] = null;
            }
            else
            {
                var temp = _items[toIndex];
                _items[toIndex] = _items[fromIndex];
                _items[fromIndex] = temp;
            }

            OnSlotChanged?.Invoke(fromIndex);
            OnSlotChanged?.Invoke(toIndex);
        }

        public void UseItem(int index)
        {
            if (_items[index] == null) return;
            Debug.Log(_items[index].data.name + " Used");
            // _items[index].Use();
            RemoveItem(index);
            // OnSlotChanged?.Invoke(index);
        }
    }
}