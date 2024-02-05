using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectBox;
    [SerializeField] private Image _switchBox;
    public int ItemID {get; private set;}
    public Item[] ItemsCollection { get; private set; }
    public Item Item
    {
        get {return ItemsCollection?[ItemID];}
    }

    public void Init(Item[] itemsCollection, int itemID)
    {
        ItemID = itemID;
        ItemsCollection = itemsCollection;
    }

    public void Render()
    {
        _icon.sprite = Item != null? Item.Icon : default;
    }

    public void Select(bool isSelect)
    {
        _selectBox.gameObject.SetActive(isSelect);
    }
    public void Switch(bool isSwitch)
    {
        _switchBox.gameObject.SetActive(isSwitch);
    }
}
