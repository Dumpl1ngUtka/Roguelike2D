using System.Collections.Generic;
using UnityEngine;

public class MenuButtonList : MonoBehaviour
{
    [SerializeField] private List<MenuButton> _menuButtons;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    private int _currentButtonIndex;
    public void SelectButton(int buttonIndex)
    {
        _currentButtonIndex = buttonIndex;
        ChangeButtonsColor(_currentButtonIndex);
        OpenButtonMenu(_currentButtonIndex);
    }

    private void ChangeButtonsColor(int buttonIndex)
    {
        foreach (var button in _menuButtons)
            button.Render(_defaultColor);
        _menuButtons[buttonIndex].Render(_selectedColor);
    }

    private void OpenButtonMenu(int buttonIndex)
    {
        foreach (var button in _menuButtons)
            button.CloseMenu();
        _menuButtons[buttonIndex].OpenMenu();
    }
}
