using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum OpenType
    {
        Inventory,
        Settings
    }

    [SerializeField] private MenuButtonList _menuButtonList;
    private PlayerInputSystem _playerInputSystem;
    public bool IsOpen { get; private set; } = false;

    private void Awake()
    {
        _playerInputSystem = new PlayerInputSystem();
        _playerInputSystem.UI.CloseMenu.performed += ctx => Close();
    }

    public void Open(OpenType type = OpenType.Inventory)
    {
        IsOpen = true;
        _playerInputSystem.Enable();
        _menuButtonList.gameObject.SetActive(true);
        switch (type)
        {
            case OpenType.Inventory:
                _menuButtonList.SelectButton(0);
                break;
            case OpenType.Settings:
                _menuButtonList.SelectButton(1);
                break;
        }
    }

    public void Close()
    {
        IsOpen = false;
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
            transform.GetChild(i).gameObject.SetActive(false);
        _playerInputSystem.Disable();
    }
}
