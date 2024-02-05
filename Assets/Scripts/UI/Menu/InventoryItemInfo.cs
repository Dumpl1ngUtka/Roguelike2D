using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Image _icon;

    public void Render(Item item)
    {
        _name.text = item.Name;
        _description.text = item.Description;
        _icon.sprite = item.Icon;
    }
}
