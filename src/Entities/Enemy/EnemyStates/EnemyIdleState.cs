using Godot;
using System;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyController controller, EnemyStateMachine stateMachine, MovementComponent movementComponent) : base(controller, stateMachine, movementComponent)
    {

    }

    public override void Enter()
    {
        base.Enter();
        GD.Print("враг ожидает");
    }

    public override void Exit()
    {
        base.Exit();
        GD.Print("враг перестал ожидать");
    }
    
}