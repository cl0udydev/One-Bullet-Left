using Godot;
using System;

public partial class MovementComponent : Node
{
	private CharacterBody2D _parentBody;

	[Export] public float Speed { get; set; } = 1750.0f;

	public override void _Ready()
	{
		_parentBody = GetParent<CharacterBody2D>();
	}

	public override void _Process(double delta)
	{
	}

	public void Move(Vector2 inputDirection)
	{
		_parentBody.Velocity = inputDirection * Speed;
		_parentBody.MoveAndSlide();
	}
}
