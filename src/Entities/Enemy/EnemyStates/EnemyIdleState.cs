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

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (_enemyController.PlayerRef != null)
        {
            _enemyStateMachine.ChangeState(new EnemyChaseState(_enemyController, _enemyStateMachine, _movementComponent));
        }
    }
}