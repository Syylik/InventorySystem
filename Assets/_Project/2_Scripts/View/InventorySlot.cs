using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Game.Inventory
{
    public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;

        public int index { get; private set; }
        private InventoryView _inventoryView;
        private InventoryItem _currentItem;
        private Transform _originalParent;
        private Transform _temporaryParent;

        private RectTransform _rectTransform;

        private void Awake() => _rectTransform = GetComponent<RectTransform>();

        public void Init(int index, InventoryView ui, Transform inventoryPanel)
        {
            this.index = index;
            _inventoryView = ui;
            _temporaryParent = inventoryPanel;
            Clear();
        }

        public void SetItem(InventoryItem item)
        {
            _currentItem = item;
            _icon.sprite = item.data.icon;
            _icon.enabled = true;
            _amountText.text = item.data.isStackable && item.amount > 1 ? item.amount.ToString() : "";
        }

        public void Clear()
        {
            _currentItem = null;
            _icon.enabled = false;
            _icon.sprite = null;
            _amountText.text = "";
        }

        // Drag & Drop
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_currentItem == null) return;

            _originalParent = _icon.transform.parent;
            _icon.transform.SetParent(_temporaryParent.transform);
            _icon.raycastTarget = false;
            // transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_currentItem == null) return;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _temporaryParent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint))
            {
                _icon.rectTransform.anchoredPosition = localPoint;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_currentItem == null) return;

            _icon.transform.SetParent(_originalParent);
            _icon.transform.localPosition = Vector3.zero;
            _icon.raycastTarget = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var droppedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();
            Debug.Log($"{index} -> {droppedSlot.index}");
            if (droppedSlot != null && droppedSlot != this) _inventoryView.OnSwap(droppedSlot.index, index);
        }

        // Use on click
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2 && _currentItem != null) _inventoryView.OnUse(index);
        }

        // Tooltip on hover
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_currentItem != null) Tooltip.Show(_currentItem.data);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tooltip.Hide();
        }
    }
}