using UnityEngine;

public class JumpState : BaseMoveState
{
    public JumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        AvailableJumpCount--;
        Jump();
    }

    public override void Exit()
    {
    }

    public override void Input()
    {
        base.Input();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (IsGrounded)
            StateMachine.ChangeState(Player.MoveState);
    }

    private void Jump()
    {
        MovementDirection.y = Stats.JumpPower;
        Debug.Log(MovementDirection.y);
    }
}
