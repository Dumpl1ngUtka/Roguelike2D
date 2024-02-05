using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] private DroppedItem _droppedItemPrefab;

    public Weapon[] Weapons { get; private set; } = new Weapon[2];
    public ItemForSecondHand[] SecondHandItems { get; private set; } = new ItemForSecondHand[2];
    public Armor[] Armors { get; private set; } = new Armor[2];
    public ConsumablesItem[] ConsumablesItems { get; private set; } = new ConsumablesItem[8];
    public Ring[] Rings { get; private set; } = new Ring[2];

    public event Action InventoryChanged;

    public Item[] GetItemCollection<T>(T item)
    {
        if (item as Weapon) return Weapons;
        else if (item as ItemForSecondHand) return SecondHandItems;
        else if (item as Armor) return Armors;
        else if (item as Ring) return Rings;
        return ConsumablesItems;
    }

    public void SwitchItems<T>(T[] collection, int indexItem1, int indexItem2)
    {
        var tmp = collection[indexItem1];
        collection[indexItem1] = collection[indexItem2];
        collection[indexItem2] = tmp;
        InventoryChanged?.Invoke();
    }

    public void AddItem<T>(T item, int itemID)
    {
        Item[] items = GetItemCollection(item);
        if (items[itemID] != null)
            DropItem(items, itemID);
        items[itemID] = item as Item;
        InventoryChanged?.Invoke();
    }

    public bool TryAddItem<T>(T item, int itemID)
    {
        Item[] items = GetItemCollection(item);
        if (items[itemID] != null)
            return false;
        items[itemID] = item as Item;
        InventoryChanged?.Invoke();
        return true;
    }

    public void DropItem<T>(T[] items, int itemID)
    {
        var droppedItem = Instantiate(_droppedItemPrefab, transform.position, Quaternion.identity);
        droppedItem.SetItem(items[itemID] as Item);
        items[itemID] = default;
        InventoryChanged?.Invoke();
    }
}
