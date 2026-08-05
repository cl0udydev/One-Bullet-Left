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
        _enemyController.EnemySprite.Play("idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (_enemyController.PlayerRef != null)
        {
            _enemyStateMachine.ChangeStateByName("chase");
        }
    }
}