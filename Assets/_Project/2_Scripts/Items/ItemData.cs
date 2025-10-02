using UnityEngine;

namespace Game.Inventory
{
    public enum ItemType
    {
        Quest,
        Material,
        Consumable,
        Weapon
    }

    [CreateAssetMenu(fileName = "Item", menuName = "SO/Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;
        public bool isStackable;
        // public int maxInStack = 16;
        public ItemType itemType;
    }
}