using Godot;
using System;

public abstract class EnemyState
{
    protected EnemyController _enemyController;
    protected EnemyStateMachine _enemyStateMachine;
    protected MovementComponent _movementComponent;

    public EnemyState(EnemyController controller, EnemyStateMachine stateMachine, MovementComponent movementComponent)
    {
        _enemyController = controller;
        _enemyStateMachine = stateMachine;
        _movementComponent = movementComponent;
    }
    public virtual void Enter() {}
    public virtual void Exit() {}
    public virtual void PhysicsUpdate(double delta) {}
}