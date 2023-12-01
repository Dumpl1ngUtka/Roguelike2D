using UnityEngine;

public abstract class BaseMoveState : State
{
    protected Vector2 MovementDirection;
    private Vector2 _playerInputDirection;
    protected bool IsCanDodge;
    protected bool IsGrounded;
    protected int AvailableJumpCount = 2;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    public BaseMoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        _rigidbody = Player.Rigidbody;
        _collider = Player.Collider;
    }

    public override void Input()
    {
        if (InputSystem.Movement.Jump.triggered)
            StateMachine.ChangeState(Player.JumpState);
        if (InputSystem.Movement.Dodge.triggered)
            StateMachine.ChangeState(Player.DodgeState);
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
        CheckCollision();
        Gravity();
    }

    private void CheckCollision()
    {
        Physics2D.queriesStartInColliders = false;
        bool groundHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.down, Stats.GrounderDistance, ~Player.GroundLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_collider.bounds.center, _collider.size, _collider.direction, 0, Vector2.up, Stats.GrounderDistance, ~Player.GroundLayer);
        if (ceilingHit)
            MovementDirection.y = Mathf.Min(0, MovementDirection.y);
        if (!IsGrounded && groundHit)
        {
            IsGrounded = true;
            AvailableJumpCount = 2;
        }
        else if (IsGrounded && !groundHit)
        {
            IsGrounded = false;
        }
        if (!IsCanDodge && groundHit)
        {
            IsCanDodge = true;
        }

        Physics2D.queriesStartInColliders = true;
    }
    private void Gravity()
    {
        if (IsGrounded && MovementDirection.y < 0)
        {
            MovementDirection.y = 0;
        }
        else
        {
            var inAirGravity = Stats.FallAcceleration;
            var maxFallSpeed = _playerInputDirection.y < 0 ?
                Stats.MaxFallSpeed * Stats.FastFallMaxSpeedModifier : Stats.MaxFallSpeed;
            MovementDirection.y = Mathf.MoveTowards(MovementDirection.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    protected void ApplyMovement()
    {
        _rigidbody.velocity = MovementDirection;
    }
}
