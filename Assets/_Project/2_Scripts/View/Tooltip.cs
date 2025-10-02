using TMPro;
using UnityEngine;

namespace Game.Inventory
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private GameObject panel;

        private static Tooltip _instance;

        private void Awake() => _instance = this;

        public static void Show(ItemData item)
        {
            _instance.title.text = item.itemName;
            _instance.description.text = item.description;
            _instance.panel.SetActive(true);
        }

        public static void Hide() => _instance.panel.SetActive(false);
    }
}