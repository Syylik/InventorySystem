using Game.Inventory;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InventoryOpener : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private InventoryView _view;
    private Animator _anim;
    private bool _isOpened = false;

    private static int IsOpenedBoolAnim = Animator.StringToHash("isOpened");

    private void Awake()
    {
        _view = GetComponent<InventoryView>();
        _anim = GetComponent<Animator>();
    }

    public void Toggle()
    {
        if (_isOpened) Close();
        else Open();
    }

    public void Open()
    {
        if (_isOpened) return;
        _isOpened = true;
        _panel.SetActive(true);
        _anim.SetBool(IsOpenedBoolAnim, true);
        _view.RefreshAll();
    }

    public void Close()
    {
        if (!_isOpened) return;
        _isOpened = false;
        _anim.SetBool(IsOpenedBoolAnim, false);
    }

    public void OnCloseAnimation() => _panel.SetActive(false);
}