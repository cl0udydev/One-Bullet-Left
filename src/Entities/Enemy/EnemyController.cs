using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
	public CharacterBody2D PlayerRef { get; set; }
	private EnemyStateMachine _stateMachine;
	private MovementComponent _movementComponent;

    public override void _Ready()
    {
        _stateMachine = GetNode<EnemyStateMachine>("EnemyStateMachine");
		_movementComponent = GetNode<MovementComponent>("MovementComponent");

		_stateMachine.Initialize(new EnemyIdleState(this, _stateMachine, _movementComponent));
    }

	public void OnVisibilityAreaBodyEntered(Node body)
	{
		if (body is PlayerController player)
		{
			PlayerRef = player;
			GD.Print("Враг заметил движение!");
		}
	}

	public void OnVisibilityAreaBodyExited(Node body)
	{
		if (body == PlayerRef)
		{
			PlayerRef = null;
			GD.Print("Игрок скрылся из виду");
		}
	}

}
