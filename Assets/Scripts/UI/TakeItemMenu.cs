using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeItemMenu : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InventoryItemInfo _mainItem;
    [SerializeField] private InventoryItemInfo _takeItem;
    [SerializeField] private InventoryItemInfo _additionalItem;

    private int _currentIndex = -1;
    private int _collectionIndex = 0;
    private Item _item;
    private Item[] _collection;
    private PlayerInputSystem _playerInputSystem;

    public Action TookItem;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();

        _playerInputSystem.UI.ChooseItem.started += ctx => ReadInput();
        _playerInputSystem.UI.Accept.started += ctx => Take();
        _playerInputSystem.UI.ChooseTab.started += ctx => ChooseTab();
    }

    private void ChooseTab()
    {
        var input = (int)_playerInputSystem.UI.ChooseTab.ReadValue<float>();
        _collectionIndex += input > 0? 1 : -1;
        _collectionIndex = Mathf.Clamp(_collectionIndex, 0, _collection.Length / 2 - 1);
        Render();
    }

    private void ReadInput()
    {
        var input = _playerInputSystem.UI.ChooseItem.ReadValue<Vector2>().x;
        _currentIndex = input > 0 ? 1 : 0;
    }

    public void SetTakeItem(Item item)
    {
        _item = item;
        _collection = _inventory.GetItemCollection(item);
        Render();
    }

    private void Render()
    {
        _mainItem.Render(_collection[_collectionIndex * 2]);
        _takeItem.Render(_item);
        _additionalItem.Render(_collection[_collectionIndex * 2 + 1]);
    }

    private void OnEnable()
    {
        _currentIndex = -1;
        _collectionIndex = 0;
        _playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void Take()
    {
        if (_currentIndex != -1)
        {
            _inventory.AddItem(_item, _currentIndex);
            _item = null;
            TookItem?.Invoke();
        }
    }
}
