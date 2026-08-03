using Godot;
using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyController controller, EnemyStateMachine stateMachine, MovementComponent movementComponent) : base(controller, stateMachine, movementComponent)
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

        if (_enemyController.PlayerRef == null)
        {
            _enemyStateMachine.ChangeState(new EnemyIdleState(_enemyController, _enemyStateMachine, _movementComponent));
            return;
        }

        Godot.Vector2 targetPosition = _enemyController.PlayerRef.GlobalPosition;
        Godot.Vector2 direction = _enemyController.GlobalPosition.DirectionTo(targetPosition);

        if (_enemyController.AttackArea.OverlapsBody(_enemyController.PlayerRef))
        {
            _enemyStateMachine.ChangeState(new EnemyAttackState(_enemyController, _enemyStateMachine, _movementComponent));
            return;
        }
        _movementComponent.Move(direction);
    }
    
}