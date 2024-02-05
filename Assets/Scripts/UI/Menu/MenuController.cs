using UnityEngine;

public class MenuController : MonoBehaviour
{
    public enum OpenType
    {
        Inventory,
        Settings,
        TakeItem,
    }

    [SerializeField] private MenuButtonList _menuButtonList;
    [SerializeField] private TakeItemMenu _takeItemMenu;
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
        if (type != OpenType.TakeItem)
        {
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
        else
        {
            _takeItemMenu.gameObject.SetActive(true);
            _takeItemMenu.TookItem += Close;
        }
    }

    public void Close()
    {
        IsOpen = false;
        _takeItemMenu.TookItem -= Close;
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
            transform.GetChild(i).gameObject.SetActive(false);
        _playerInputSystem.Disable();
    }
}
