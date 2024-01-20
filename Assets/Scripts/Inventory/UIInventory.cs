using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo _itemInfo;
    [SerializeField] private InventoryCell _inventoryCellPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Inventory _playerInventory;

    private List<InventoryCell> _renderedItemCells;
    private PlayerInputSystem _playerInputSystem;
    private int _currentItemIndex = 0;
    private const int _columnCount = 4;


    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.UI.ChooseItem.started += ctx => ReadInput();
        _playerInputSystem.UI.EquipItem.performed += ctx => Equip();
        _playerInputSystem.UI.DropItem.performed += ctx => Drop();
    }

    private void ReadInput()
    {
        var input = _playerInputSystem.UI.ChooseItem.ReadValue<Vector2>();
        var newItemIndex = _currentItemIndex + (int)input.x - (int)input.y * _columnCount;
        newItemIndex = Mathf.Clamp(newItemIndex, 0, _renderedItemCells.Count - 1);
        SelectItem(newItemIndex);
    }

    private void OnEnable()
    {
        _playerInputSystem.Enable();
        RenderItems(_playerInventory.Items);
        _playerInventory.InventoryChanged += RenderItems;
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
        _playerInventory.InventoryChanged -= RenderItems;
    }

    private void SelectItem(int itemIndex)
    {
        if (itemIndex >= _renderedItemCells.Count || itemIndex < 0)
            return;

        _renderedItemCells[_currentItemIndex].Select(false);
        _currentItemIndex = itemIndex;  
        _renderedItemCells[_currentItemIndex].Select(true);
        var item = _renderedItemCells[_currentItemIndex].Item;
        if (item != null)
            _itemInfo.Render(item);
    }

    public void UpdateItemsList(List<InventoryCell> items)
    {
        _renderedItemCells = items;
        _currentItemIndex = 0;
        SelectItem(0);
    }

    private void Equip()
    {
        var itemID = _renderedItemCells[_currentItemIndex].ItemID;
        _playerInventory.Equip(itemID);
    }

    private void Drop()
    {
        var itemID = _renderedItemCells[_currentItemIndex].ItemID;
        _playerInventory.DropItem(itemID);
    }

    private void RenderItems(Dictionary<int, Item> items)
    {
        ClearContainer();
        var renderedItems = new List<InventoryCell>();
        foreach (var item in items)
        {
            var cell = Instantiate(_inventoryCellPrefab, _container);
            renderedItems.Add(cell);
            cell.Render(item.Value, item.Key);
        }
        UpdateItemsList(renderedItems);
    }

    private void ClearContainer()
    {
        int childs = _container.childCount;
        for (int i = childs - 1; i >= 0; i--)
            Destroy(_container.GetChild(i).gameObject);
    }
}

