using Godot;
using System;

public enum SoundType
{
	Shot,
}

public partial class EventBus : Node
{
    public static event Action<SoundType> SoundRequested;

    public static void EmitSound(SoundType type)
    {
        SoundRequested?.Invoke(type);
    }
}
