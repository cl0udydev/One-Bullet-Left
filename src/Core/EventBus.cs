using Godot;
using System;

public enum SoundType
{
	Shot,
    PlayerAttacked,
    EnemyAttacked,
}

public partial class EventBus : Node
{
    public static event Action<SoundType> SoundRequested;
    public static event Action<Node> EntityDied;
    public static event Action<int> PlayerHealthChanged;

    public static void EmitSound(SoundType type)
    {
        SoundRequested?.Invoke(type);
    }

    public static void EmitDeath(Node entity)
    {
        EntityDied?.Invoke(entity);
    }

    public static void EmitPlayerHealthChanged(int currentHealth)
    {
        PlayerHealthChanged?.Invoke(currentHealth);
    }

}
