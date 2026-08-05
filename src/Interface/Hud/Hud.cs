using Godot;
using System;
using System.Collections.Generic;

public partial class Hud : CanvasLayer
{
    [Export] private PackedScene _heartSlotScene;
    private BulletSlot _bulletSlotSprite;
    private HBoxContainer _heartsContainer;
    private List<HeartSlot> _heartSlots = new();
    private ColorRect _damageFlash;
    private int _lastHealth = 3;

    public override void _Ready()
    {
        _damageFlash = GetNode<ColorRect>("DamageFlash");
        _heartsContainer = GetNode<HBoxContainer>("GameplayUI/HBoxContainer/HeartsContainer");
        _bulletSlotSprite = GetNode<BulletSlot>("GameplayUI/HBoxContainer/BulletSlot");

        EventBus.PlayerHealthChanged += OnPlayerHealthChanged;
        EventBus.ChangingBulletAvailability += ChangeBulletSlot;
        

        for (int i = 0; i < 3; i++)
        {
            HeartSlot newHeart = _heartSlotScene.Instantiate<HeartSlot>();
            _heartsContainer.AddChild(newHeart);
            _heartSlots.Add(newHeart);
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        EventBus.PlayerHealthChanged -= OnPlayerHealthChanged;
        EventBus.ChangingBulletAvailability -= ChangeBulletSlot;
    }

    private void OnPlayerHealthChanged(int currentHealth)
    {
        if (currentHealth < _lastHealth)
        {
            TriggerDamageFlash();
        }

        _lastHealth = currentHealth;

        for (int i = 0; i < _heartSlots.Count; i++)
        {
            if (i >= currentHealth)
            {
                if (!_heartSlots[i].IsBroken)
                {
                    _heartSlots[i].PlayDamage();
                }
            }
            else
            {
                _heartSlots[i].ResetToFull();
            }
        }
    }

    private void TriggerDamageFlash()
    {
        if (_damageFlash == null) return;

        Tween flashTween = CreateTween();

        _damageFlash.Modulate = new Color(1f, 0f, 0f, 0.5f);

        flashTween.TweenProperty(_damageFlash, "modulate", new Color(1f, 1f, 1f, 0f), 0.4f)
        .SetTrans(Tween.TransitionType.Cubic) 
        .SetEase(Tween.EaseType.Out);
    }

    private void ChangeBulletSlot(bool HaveBullet)
    {
        if (HaveBullet)
        {
            _bulletSlotSprite.PlayFull();
        }
        else
        {
            _bulletSlotSprite.PlayHollow();
        }
    }
}
