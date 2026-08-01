using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed { get; set; }
	
	private Vector2 _direction;

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		GlobalPosition += _direction * Speed;
    }

	public void SetDirection(Vector2 dir) 
	{ 
		_direction = dir;
	}

}
