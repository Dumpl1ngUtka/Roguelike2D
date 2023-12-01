using UnityEngine;

public class MoveState : BaseMoveState
{
    private Vector2 _playerInputDirection;
    public MoveState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        _playerInputDirection = Vector2.zero;
    }

    public override void Exit()
    {
    }

    public override void Input()
    {
        base.Input();
        _playerInputDirection = InputSystem.Movement.Move.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        HorizontalMove();
        ApplyMovement();
    }
    private void HorizontalMove()
    {
        if (_playerInputDirection.x == 0)
        {
            var deceleration = IsGrounded ? Stats.GroundDeceleration : Stats.AirDeceleration;
            MovementDirection.x = Mathf.MoveTowards(MovementDirection.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            var maxSpeed = Stats.MaxSpeed;
            MovementDirection.x = Mathf.MoveTowards(MovementDirection.x, _playerInputDirection.x * maxSpeed, Stats.Acceleration * Time.fixedDeltaTime);
        }
    }
}
