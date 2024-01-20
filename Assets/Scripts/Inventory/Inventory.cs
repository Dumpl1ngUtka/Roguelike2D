using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<int, Item> Items = new Dictionary<int, Item>();
    private List<int> _idList = new List<int>();
    private int _idCount = 100;
    [SerializeField] private Item _item;
    [SerializeField] private Item _item2;

    public int WeaponIndex { get; private set; } = -1;
    public int SecondHandItemIndex { get; private set; } = -1;
    public int ArmorIndex { get; private set; } = -1;

    public event Action<Dictionary<int, Item>> InventoryChanged;

    private void Awake()
    {
        for (int i = 0; i < _idCount; i++)
            _idList.Add(i);

        AddItem(_item);
        AddItem(_item);
        AddItem(_item2);
        AddItem(_item2);
        AddItem(_item2);
        AddItem(_item2);
        AddItem(_item);
        AddItem(_item);
    }

    public bool Equip(int itemIndex)
    {
        var item = Items[itemIndex];
        if (item as Weapon)
        {
            WeaponIndex = itemIndex;
            return true;
        }
        if (item as ItemForSecondHand)
        {
            SecondHandItemIndex = itemIndex;
            return true;
        }
        if (item as Armor)
        {
            ArmorIndex = itemIndex;
            return true;
        }
        return false;
    }

    public void AddItem(Item item)
    {
        Items.Add(_idList[0], item);
        _idList.Remove(_idList[0]);
        InventoryChanged?.Invoke(Items);
    }

    public void DropItem(int itemID)
    {
        Items.Remove(itemID);
        _idList.Add(itemID);
        if (SecondHandItemIndex == itemID)
            SecondHandItemIndex = -1;
        if (ArmorIndex == itemID)
            ArmorIndex = -1;
        InventoryChanged?.Invoke(Items);
    }
}
