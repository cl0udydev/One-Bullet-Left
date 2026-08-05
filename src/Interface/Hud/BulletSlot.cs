using Godot;
using System;

public partial class BulletSlot : Control
{
    private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("BulletSprite");
    }

	public void PlayFull()
	{
		_sprite.Play("full");
	}

	public void PlayHollow()
	{
		_sprite.Play("hollow");
	}
}
