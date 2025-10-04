using System.Collections.Generic;
using Game.Inventory;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] Inventory _inventory;
    [SerializeField] InventoryView _inventoryView;
    [SerializeField] InventoryOpener _inventoryOpener;
    private InventorySave _inventorySave;

    [SerializeField] private List<ItemData> _startupItems;

    public void Awake()
    {
        _inventorySave = new InventorySave();
        if (_inventorySave.HasSave) _inventory = new(_inventorySave.Load());
        else _inventory = new Inventory();
        _inventoryView.Init(_inventory);

        Debug.Log("Init Finished. Slots = " + _inventory.Capacity);
    }

    private void Start()
    {
        foreach (ItemData item in _startupItems) _inventory.TryAddItem(item);
        _inventory.TryAddItem(_startupItems[0], 5, 5);
        _inventory.TryAddItem(_startupItems[0], 5, 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _inventoryOpener.Toggle();
        if (Input.GetKeyDown(KeyCode.A)) _inventory.TryAddItem(_startupItems[0]);
        if (Input.GetKeyDown(KeyCode.D)) _inventory.TryAddItem(_startupItems[1]);
        if (Input.GetKeyDown(KeyCode.S)) _inventorySave.Save(_inventory); 
    }
}