using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed { get; set; }
	private bool _isFlying = true;
	[Export] public int Damage { get; set; } = 1;
	private Vector2 _direction;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		if (_isFlying)
		{
		GlobalPosition += _direction * Speed;
		}
    }

	public void SetDirection(Vector2 dir) 
	{ 
		_direction = dir;
	}

	public void OnBodyEntered(Node body)
	{
		if (_isFlying)
		{
			HealthComponent enemyHealth = body.GetNodeOrNull<HealthComponent>("HealthComponent");
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(Damage);
				_isFlying = false;
				GlobalPosition -= _direction * 35f; 
			}
		}
		else
		{
			WeaponComponent playerWeapon = body.GetNodeOrNull<WeaponComponent>("WeaponComponent");
			if (playerWeapon != null)
			{
				playerWeapon.ReturnBullet();
				QueueFree();
			}
		}
	}

}
