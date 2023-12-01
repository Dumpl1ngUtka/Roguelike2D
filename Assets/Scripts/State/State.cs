using UnityEngine;

public abstract class State
{
    protected StateMachine StateMachine;
    protected PlayerInputSystem InputSystem;
    protected Player Player;
    protected MoveStats Stats;

    protected State(Player player, StateMachine stateMachine)
    {
        Player = player;
        InputSystem = player.InputSystem;
        Stats = player.Stats;
        StateMachine = stateMachine;
    }
    public abstract void Enter();

    public abstract void Input();

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void Exit();
}
