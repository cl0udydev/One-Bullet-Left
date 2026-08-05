using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
	public CharacterBody2D PlayerRef { get; set; }
	private EnemyStateMachine _stateMachine;
	private MovementComponent _movementComponent;
	private HealthComponent _healthComponent;
	public AnimatedSprite2D EnemySprite;
	private Area2D _attackArea;
	public Area2D AttackArea => _attackArea;
	


    public override void _Ready()
    {
        _stateMachine = GetNode<EnemyStateMachine>("EnemyStateMachine");
		_movementComponent = GetNode<MovementComponent>("MovementComponent");
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		EnemySprite = GetNode<AnimatedSprite2D>("EnemySprite");
		_attackArea = GetNode<Area2D>("AttackArea");

		_healthComponent.OnDied += HandleDeath;

		_stateMachine.SetupStates(this, _movementComponent);
    }
	

    public override void _ExitTree()
    {
        base._ExitTree();
		_healthComponent.OnDied -= HandleDeath;
		EventBus.EmitSound(SoundType.EnemyAttacked);
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
