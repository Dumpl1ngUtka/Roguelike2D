using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _weight;
    [SerializeField] private Image _selectBox;
    public Item Item { get; private set; }
    public int ItemID { get; private set; }

    public void Render(Item item, int itemID)
    {
        Item = item;
        ItemID = itemID;
        _icon.sprite = item.Icon;
        _weight.text = item.Weight.ToString();
    }

    public void Select(bool isSelect)
    {
        _selectBox.gameObject.SetActive(isSelect);
    }
}
