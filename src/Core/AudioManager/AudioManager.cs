using Godot;
using System;
using System.Collections.Generic;

public partial class AudioManager : Node
{
    private Dictionary<SoundType, AudioStream> _soundLibrary = new();
    private List<AudioStreamPlayer> _audioPlayers = new();

    public override void _Ready()
    {
        _soundLibrary.Add(SoundType.Shot, GD.Load<AudioStream>("res://assets/Sounds/gunshot.wav"));

        foreach (Node child in GetChildren())
        {
            if (child is AudioStreamPlayer player)
            {
                _audioPlayers.Add(player);
            }
        }

        EventBus.SoundRequested += PlaySound;
    }

	public override void _ExitTree()
    {
        EventBus.SoundRequested -= PlaySound;
    }

    private void PlaySound(SoundType type)
    {
        if (!_soundLibrary.ContainsKey(type))
        {
            return;
        }

        AudioStream streamToPlay = _soundLibrary[type];

        AudioStreamPlayer freePlayer = FindFreePlayer();

        if (freePlayer != null)
        {
            freePlayer.Stream = streamToPlay;
            freePlayer.Play();
        }
    }

    private AudioStreamPlayer FindFreePlayer()
    {
        foreach (var player in _audioPlayers)
        {
            if (!player.Playing)
            {
                return player;
            }
        }
        return null;
	}
}