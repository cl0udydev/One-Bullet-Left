using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySpawner : Node
{
	private Godot.Collections.Array<Node> _spawnMarkers = new () {};
	private Timer _timer;
	[Export] public PackedScene EnemyScene { get; set; }
	[Export] private int maxAmount;
	private int currentAmount;

    public override void _Ready()
	{
		_timer = GetNode<Timer>("Timer");

		_spawnMarkers = GetTree().GetNodesInGroup("EnemyMarker");
	}

	public void OnTimerTimeout()
	{
		int randomIndex = GD.RandRange(0, _spawnMarkers.Count - 1);
		var CurrentMarker = _spawnMarkers[randomIndex];

		Node newObject = EnemyScene.Instantiate();
		AddChild(newObject);

		if (newObject is Node2D enemy2D && CurrentMarker is Node2D marker2D)
		{
			enemy2D.GlobalPosition = marker2D.GlobalPosition;
			currentAmount += 1;

			if (currentAmount == maxAmount)
			{
				_timer.Stop();
			}
		}

		
	}

}
