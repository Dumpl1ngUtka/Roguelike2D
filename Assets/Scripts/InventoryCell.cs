using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _weight;
    public void Render(Item item)
    {
        _icon.sprite = item.Icon;
        _name.text = item.Name;
        _weight.text = item.Weight.ToString();
    }
}
