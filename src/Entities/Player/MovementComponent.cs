using Godot;
using System;

public partial class MovementComponent : Node
{
	[Export] private CharacterBody2D BodyToMove;

	[Export] public float Speed { get; set; }

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void Move(Vector2 inputDirection)
	{
		BodyToMove.Velocity = inputDirection * Speed;
		BodyToMove.MoveAndSlide();
	}
}
