using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] InventoryView _inventoryView;

    [SerializeField] private List<ItemData> _startupItems;

    public void Awake()
    {
        _inventory = new Inventory();
        _inventoryView.Init(_inventory);

        Debug.Log("Init Finished. Slots = " + _inventory.Capacity);
    }

    private void Start()
    {
        foreach(ItemData item in _startupItems) _inventory.TryAddItem(item);
    }
}