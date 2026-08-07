using Godot;
using System;

public partial class Bullet : Area2D
{
	[Export] public float Speed { get; set; }
	private bool _isFlying = true;
	[Export] public int Damage { get; set; } = 1;
	private Vector2 _direction;
	private CollisionShape2D _bulletCollision;

    public override void _Ready()
    {
        _bulletCollision = GetNode<CollisionShape2D>("BulletCollision");
    }


    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		if (_isFlying)
		{
		GlobalPosition += _direction * Speed * (float)delta;
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
			if (body is PlayerController) 
			{
				return;
			}

			if (body is TileMapLayer)
			{
				_isFlying = false;
				GlobalPosition -= _direction * 35f;
			}

			HealthComponent enemyHealth = body.GetNodeOrNull<HealthComponent>("HealthComponent");
			if (enemyHealth != null)
			{
				enemyHealth.TakeDamage(Damage);
				_isFlying = false;
				GlobalPosition -= _direction * 35f;
			}

			_bulletCollision.Scale = new Vector2(3.0f, 3.0f);
		}
		else
		{
			WeaponComponent playerWeapon = body.GetNodeOrNull<WeaponComponent>("WeaponComponent");
			if (playerWeapon != null)
			{
				playerWeapon.ReturnBullet();
				_bulletCollision.Scale = new Vector2(1.0f, 1.0f);
				QueueFree();
			}
		}
	}

}
