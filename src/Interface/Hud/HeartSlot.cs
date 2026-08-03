using Godot;
using System;

public partial class HeartSlot : Control
{
    private AnimatedSprite2D _sprite;
    private bool _isBroken = false;
    public bool IsBroken => _isBroken;


    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        _sprite.AnimationFinished += OnAnimationFinished;
    }

    public void PlayDamage() 
    {
        if (_isBroken) return; 

        _isBroken = true; 
        _sprite.Play("damaged");
    }

    public void ResetToFull() 
    {
        _isBroken = false; 
        _sprite.Play("full");
    }


    private void OnAnimationFinished()
    {
        if (_sprite.Animation == "damaged")
        {
            _sprite.Play("empty");
        }
    }
}
