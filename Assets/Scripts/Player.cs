using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public StateMachine MoveStateMachine;
    public MoveState MoveState;
    public JumpState JumpState;
    public DodgeState DodgeState;
    public PlayerInputSystem InputSystem;
    public Rigidbody2D Rigidbody;
    public CapsuleCollider2D Collider;


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
    private void Awake()
    {
        InputSystem = new PlayerInputSystem();
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
    }

    private void OnEnable()
    {
        InputSystem.Enable();
    }

    private void OnDisable()
    {
        InputSystem.Disable();
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
    private void Start()
    {
        MoveStateMachine = new StateMachine();

        MoveState = new MoveState(this, MoveStateMachine);
        JumpState = new JumpState(this, MoveStateMachine);
        DodgeState = new DodgeState(this, MoveStateMachine);

        MoveStateMachine.Initialize(MoveState);
    }

    private void Update()
    {
        MoveStateMachine.CurrentState.Input();

        MoveStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        MoveStateMachine.CurrentState.PhysicsUpdate();
    }
}
