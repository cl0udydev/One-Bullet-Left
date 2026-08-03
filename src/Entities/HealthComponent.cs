using Godot;
using System;
using System.Data;

public partial class HealthComponent : Node
{
    [Export] public int MaxHealth;
    [Export] private bool _isPlayer = false;
    public int CurrentHealth;
    public event Action OnDied;

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        if (_isPlayer)
        {
            EventBus.EmitPlayerHealthChanged(CurrentHealth);
        }
        if (CurrentHealth == 0)
        {
            OnDied?.Invoke();
        }
    }
}