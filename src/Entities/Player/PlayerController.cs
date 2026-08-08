using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	private MovementComponent _movementComponent;
	private WeaponComponent _weaponComponent;
	private HealthComponent _healthComponent;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		_movementComponent = GetNode<MovementComponent>("MovementComponent");
		_weaponComponent = GetNode<WeaponComponent>("WeaponComponent");
		_sprite = GetNode<Sprite2D>("PlayerSprite");
		_healthComponent = GetNode<HealthComponent>("HealthComponent");

		_healthComponent.OnDied += HandleDeath;

		EventBus.EmitPlayerHealthChanged(_healthComponent.CurrentHealth);

	}

    public override void _ExitTree()
    {
        _healthComponent.OnDied -= HandleDeath;
    }

    public override void _PhysicsProcess(double delta)
    {

		Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");

		_movementComponent.Move(inputDirection);

		Vector2 mouseVelocity = Input.GetLastMouseVelocity();

		if (mouseVelocity.Length() > 50f)
		{
			_sprite.LookAt(GetGlobalMousePosition());
			_sprite.Rotation += Mathf.DegToRad(90);
		}

		if (Input.IsActionJustPressed("attack"))
		{
			var direction = _sprite.GlobalTransform.X.Rotated(Mathf.DegToRad(-90)).Normalized();
			_weaponComponent.Shoot(direction);
		}
    }
	
	private void HandleDeath()
	{
		EventBus.EmitDeath(this);

		QueueFree();
	}

}
