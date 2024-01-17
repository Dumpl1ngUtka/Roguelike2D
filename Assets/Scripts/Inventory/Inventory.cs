using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    public ItemForSecondHand SecondHandItem { get; private set; }
    public Armor Armor { get; private set; }

    public bool Equip(Item item)
    {
        if (item as ItemForSecondHand)
        {
            SecondHandItem = (ItemForSecondHand)item;
            return true;
        }
        if (item as Armor)
        {
            Armor = (Armor)item;
            return true;
        }
        return false;
    }
}
