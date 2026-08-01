using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	private MovementComponent _movementComponent;
	private WeaponComponent _weaponComponent;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		_movementComponent = GetNode<MovementComponent>("MovementComponent");
		_weaponComponent = GetNode<WeaponComponent>("WeaponComponent");
		_sprite = GetNode<Sprite2D>("PlayerSprite");
	}

	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		_sprite.LookAt(GetGlobalMousePosition());

		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		_movementComponent.Move(inputDirection);

		if (Input.IsActionJustPressed("attack"))
		{
			var direction = _sprite.GlobalTransform.X;
			_weaponComponent.Shoot(direction);
		}
    }

}
