using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Image _colorLine;
    [SerializeField] private GameObject _menu;
    public void Render(Color color)
    {
        _colorLine.color = color;
        _buttonText.color = color;
    }

    public void OpenMenu()
    {
        _menu.SetActive(true);
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
    }
}
