using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private InventoryCell _inventoryCellPrefab;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private Transform _container;

    private void RenderItems()
    {
        foreach (Item item in _playerInventory.Items)
        {
            var cell = Instantiate(_inventoryCellPrefab, _container);
            cell.Render(item);
        }
    }

    private void OnEnable()
    {
        ClearContainer();
        RenderItems();
    }

    private void ClearContainer()
    {
        int childs = _container.childCount;
        for (int i = childs - 1; i >= 0; i--)
            Destroy(_container.GetChild(i).gameObject);
    }
}
