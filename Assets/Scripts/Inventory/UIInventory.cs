using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Item _noInfoItem;
    [SerializeField] private InventoryItemInfo _itemInfo;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private InventoryCell[] _weaponItemCells;
    [SerializeField] private InventoryCell[] _armorItemCells;
    [SerializeField] private InventoryCell[] _secondHandItemCells;
    [SerializeField] private InventoryCell[] _ringsItemCells;
    [SerializeField] private InventoryCell[] _consumablesItemCells1;
    [SerializeField] private InventoryCell[] _consumablesItemCells2;
    [SerializeField] private InventoryCell[] _consumablesItemCells3;
    [SerializeField] private InventoryCell[] _consumablesItemCells4;
    private InventoryCell[][] _itemCells = new InventoryCell[_rowCount * _columnCount][];
    private InventoryCell _takeItem = null;

    private PlayerInputSystem _playerInputSystem;
    private int _currentItemIndex = 0;
    private int _currentCellIndex = 0;
    private const int _columnCount = 2;
    private const int _rowCount = 4;

    private void Awake()
    {
        CellsInit();
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.UI.ChooseItem.started += ctx => ReadInput();
        _playerInputSystem.UI.DropItem.performed += ctx => Drop();
        _playerInputSystem.UI.Switch.performed += ctx => Switch();
        _playerInventory.InventoryChanged += RenderItems;
    }

    private void Switch()
    {
        var itemCell1 = _itemCells[_currentCellIndex][0];
        var itemCell2 = _itemCells[_currentCellIndex][1];
        _playerInventory.SwitchItems(itemCell1.ItemsCollection, itemCell1.ItemID, itemCell2.ItemID);
    }

    private void CellsInit()
    {
        var i = 0;
        PairCellsInit(_weaponItemCells, _playerInventory.Weapons, i++);
        PairCellsInit(_armorItemCells, _playerInventory.Armors, i++);
        PairCellsInit(_secondHandItemCells, _playerInventory.SecondHandItems, i++);
        PairCellsInit(_ringsItemCells, _playerInventory.Rings, i++);
        PairCellsInit(_consumablesItemCells1, _playerInventory.ConsumablesItems, i++,0);
        PairCellsInit(_consumablesItemCells2, _playerInventory.ConsumablesItems, i++,2);
        PairCellsInit(_consumablesItemCells3, _playerInventory.ConsumablesItems, i++,4);
        PairCellsInit(_consumablesItemCells4, _playerInventory.ConsumablesItems, i++,6);
    }

    private void PairCellsInit(InventoryCell[] itemCells, Item[] collection , int cellsIndex, int startIndex = 0)
    {
        for (int i = 0; i < itemCells.Length; i++)
            itemCells[i].Init(collection, i + startIndex);
        _itemCells[cellsIndex] = itemCells;
    }

    private void ReadInput()
    {
        var input = _playerInputSystem.UI.ChooseItem.ReadValue<Vector2>();
        var currentCellIndex = _currentCellIndex - (int)input.y;
        var currentItemIndex = _currentItemIndex + (int)input.x;
        if (currentItemIndex > _itemCells[0].Length - 1)
        {
            currentItemIndex = 0;
            currentCellIndex += _rowCount;
        }
        else if (currentItemIndex < 0)
        {
            currentItemIndex = _itemCells[0].Length - 1;
            currentCellIndex -= _rowCount;
        }
        if (currentCellIndex < _itemCells.Length && currentCellIndex >= 0)
            SelectItem(currentCellIndex, currentItemIndex);
    }

    private void ReadInput(List<int> cellsIndexes)
    {
        var input = _playerInputSystem.UI.ChooseItem.ReadValue<Vector2>();
        var currentCellIndex = _currentCellIndex - (int)input.y;
        var currentItemIndex = _currentItemIndex + (int)input.x;
        if (currentItemIndex >= 0 && currentItemIndex < _itemCells[0].Length)
        {
            currentItemIndex = 0;
            currentCellIndex += _rowCount;
        }
        if (cellsIndexes.Contains(currentCellIndex))
            SelectItem(currentCellIndex, currentItemIndex);
    }


    private void OnEnable()
    {
        _playerInputSystem.Enable();
        ResetUIInventory();
    }
    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void ResetUIInventory()
    {
        RenderItems();
        foreach (var itemCells in _itemCells)
            foreach (var item in itemCells)
                item.Select(false);
        SelectItem();
    }

    private void RenderItems()
    {
        foreach (var itemCell in _itemCells)
            foreach (var item in itemCell)
                item.Render();
        SelectItem(_currentCellIndex,_currentItemIndex);
    }

    private void SelectItem(int cellIndex = 0, int itemIndex = 0)
    {
        _itemCells[_currentCellIndex][_currentItemIndex].Select(false);
        _currentCellIndex = cellIndex;
        _currentItemIndex = itemIndex;
        _itemCells[_currentCellIndex][_currentItemIndex].Select(true);
        var item = _itemCells[_currentCellIndex][_currentItemIndex].Item;
        if (item == null)
            item = _noInfoItem;
        _itemInfo.Render(item);
    }

    private void Drop()
    {
        var item = _itemCells[_currentCellIndex][_currentItemIndex];
        _playerInventory.DropItem(item.ItemsCollection, item.ItemID);
    }
}

