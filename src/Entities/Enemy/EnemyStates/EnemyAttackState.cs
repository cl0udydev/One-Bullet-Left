using Godot;
using System;

public class EnemyAttackState : EnemyState
{
    public float _attackCooldown = 0.5f;
    public float _timer = 0f;
    public EnemyAttackState(EnemyController controller, EnemyStateMachine stateMachine, MovementComponent movementComponent) : base(controller, stateMachine, movementComponent)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _enemyController.EnemySprite.Play("idle");

        _timer = 0f;

        _movementComponent.Move(Vector2.Zero);

        var playerHealth = _enemyController.PlayerRef.GetNodeOrNull<HealthComponent>("HealthComponent");
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
            EventBus.EmitSound(SoundType.PlayerAttacked);
        }
    }


    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        _timer += (float)delta;

        if (_timer >= _attackCooldown)
        {
            _enemyStateMachine.ChangeState(new EnemyChaseState(_enemyController, _enemyStateMachine, _movementComponent));
        }
    }

}