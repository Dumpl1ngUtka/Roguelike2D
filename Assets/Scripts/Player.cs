using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MenuController _menu;

    public PlayerParameters Parameters;
    public LayerMask GroundLayer;
    public Condition MoveCondition;
    public Condition DodgeCondition;
    public Condition BlockCondition;
    public PlayerInputSystem InputSystem { get; private set; }
    public Condition CurrentCondition { get; private set; }
    public enum ConditionType
    {
        Move,
        Dodge,
        Block,
    }

    private void Awake()
    {
        InputSystem = new PlayerInputSystem();
        InputSystem.Enable();
        InputSystem.UI.OpenSettings.performed += ctx => StartCoroutine(OpenMenu(MenuController.OpenType.Settings));
        InputSystem.UI.OpenInventory.performed += ctx => StartCoroutine(OpenMenu(MenuController.OpenType.Inventory));
    }

    public void ChangeCurrentCondition(ConditionType type)
    {
        switch (type)
        {
            case ConditionType.Move:
                CurrentCondition = MoveCondition;
                break;
            case ConditionType.Dodge:
                CurrentCondition = DodgeCondition;
                break;
            case ConditionType.Block:
                CurrentCondition = BlockCondition;
                break;
        }
    }

    public IEnumerator OpenMenu(MenuController.OpenType type)
    {
        InputSystem.Disable();
        _menu.Open(type);
        while (_menu.IsOpen)
            yield return new WaitForEndOfFrame();
        InputSystem.Enable();
    }
}
