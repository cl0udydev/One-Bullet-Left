using Godot;
using System;
using System.Data;

public partial class HealthComponent : Node
{
    [Export] public int MaxHealth;
    private int CurrentHealth;

    public event Action OnDied;

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth == 0)
        {
            OnDied?.Invoke();
        }
    }
}