using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerParameters Parameters;
    [SerializeField] public LayerMask GroundLayer;
    public Condition MoveCondition;
    public Condition DodgeCondition;
    public Condition BlockCondition;
    public Condition CurrentCondition { get; private set; }
    public MoveStats Stats;
    public enum ConditionType
    {
        Move,
        Dodge,
        Block,
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
}
