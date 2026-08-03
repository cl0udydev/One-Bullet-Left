using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
	public CharacterBody2D PlayerRef { get; set; }
	private EnemyStateMachine _stateMachine;
	private MovementComponent _movementComponent;
	private HealthComponent _healthComponent;
	private Area2D _attackArea;
	public Area2D AttackArea => _attackArea;

    public override void _Ready()
    {
        _stateMachine = GetNode<EnemyStateMachine>("EnemyStateMachine");
		_movementComponent = GetNode<MovementComponent>("MovementComponent");
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_attackArea = GetNode<Area2D>("AttackArea");

		_healthComponent.OnDied += HandleDeath;

		_stateMachine.Initialize(new EnemyIdleState(this, _stateMachine, _movementComponent));
    }

	public void OnVisibilityAreaBodyEntered(Node body)
	{
		if (body is PlayerController player)
		{
			PlayerRef = player;
		}
	}

	public void OnVisibilityAreaBodyExited(Node body)
	{
		if (body == PlayerRef)
		{
			PlayerRef = null;
		}
	}

	private void HandleDeath()
	{
		EventBus.EmitDeath(this);

		QueueFree();
	}

}
