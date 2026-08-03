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
            _enemyStateMachine.ChangeState(new EnemyChaseState(_enemyController, _enemyStateMachine, _movementComponent));
        }
    }
}