using Godot;
using System;
using System.ComponentModel;

public partial class WeaponComponent : Node
{
	private bool _haveBullet { get; set; }
	[Export] public PackedScene BulletScene { get; set; }
	[Export] private Marker2D _marker;
	[Export] private Sprite2D _shootSprite;

	public override void _Ready()
	{
		_haveBullet = true;
	}

	public void ReturnBullet()
	{
		if (!_haveBullet)
		{
			_haveBullet = true;
			EventBus.EmitChangingBulletAvailability(_haveBullet);
		}
	}

	public async void Shoot(Vector2 direction)
	{
		if (_haveBullet)
		{
			
			Bullet newBullet = BulletScene.Instantiate<Bullet>();
			newBullet.SetDirection(direction);

			newBullet.GlobalPosition = _marker.GlobalPosition;
			newBullet.GlobalRotation = _marker.GlobalRotation;

			GetTree().Root.AddChild(newBullet);

			EventBus.EmitSound(SoundType.Shot);
			
			_shootSprite.Show();

			await ToSignal(GetTree().CreateTimer(0.05f), SceneTreeTimer.SignalName.Timeout);

			_shootSprite.Hide();

			_haveBullet = false;
			EventBus.EmitChangingBulletAvailability(_haveBullet);
		}
	}
}
