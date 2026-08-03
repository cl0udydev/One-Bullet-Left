using Godot;
using System;

public enum SoundType
{
	Shot,
    PlayerAttacked,
}

public partial class EventBus : Node
{
    public static event Action<SoundType> SoundRequested;
    public static event Action<Node> EntityDied;

    public static void EmitSound(SoundType type)
    {
        SoundRequested?.Invoke(type);
    }

    public static void EmitDeath(Node entity)
    {
        EntityDied?.Invoke(entity);
    }
}
