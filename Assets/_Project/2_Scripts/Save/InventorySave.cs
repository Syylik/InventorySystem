using System.Collections.Generic;
using System.Linq;
using Game.Inventory;
using UnityEngine;

[System.Serializable]
public class InventoryItemSave
{
    public InventoryItem[] items;

    public InventoryItemSave(InventoryItem[] items)
    {
        this.items = new InventoryItem[items.Length];
        items.CopyTo(this.items, 0);
    }
}

public class InventorySave
{
    private string InventorySaveKey = nameof(InventorySaveKey);

    public void Save(Inventory inventory)
    {
        InventoryItemSave save = new(inventory.Items.ToArray());
        string saveJson = JsonUtility.ToJson(save);
        PlayerPrefs.SetString(InventorySaveKey, saveJson);
    }

    public bool HasSave => PlayerPrefs.HasKey(InventorySaveKey);

    public InventoryItem[] Load()
    {
        var json = PlayerPrefs.GetString(InventorySaveKey);
        Debug.Log(json);
        var saveData = JsonUtility.FromJson<InventoryItemSave>(json);
        return saveData.items ?? new InventoryItem[30];
    }
}
