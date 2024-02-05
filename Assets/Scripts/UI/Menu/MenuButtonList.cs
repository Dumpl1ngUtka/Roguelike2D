using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonList : MonoBehaviour
{
    [SerializeField] private List<MenuButton> _menuButtons;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _selectedColor;
    
    private int _currentButtonIndex;
    private PlayerInputSystem _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputSystem();
        _inputActions.UI.ChooseTab.started += ctx => ReadInput();
    }

    private void ReadInput()
    {
        var input = _inputActions.UI.ChooseTab.ReadValue<float>();
        _currentButtonIndex += input > 0 ? 1 : -1;
        SelectButton(_currentButtonIndex);
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    public void SelectButton(int buttonIndex)
    {
        _currentButtonIndex = Mathf.Clamp(buttonIndex, 0, _menuButtons.Count - 1);
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
