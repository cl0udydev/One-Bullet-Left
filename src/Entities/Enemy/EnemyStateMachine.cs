using Godot;
using System;

public partial class EnemyStateMachine : Node
{
	private EnemyState _currentState;

	public void Initialize(EnemyState startingState)
	{
		_currentState = startingState;
		_currentState.Enter();
	}

	public void ChangeState(EnemyState newState)
	{
		if (_currentState != null)
		{
			_currentState.Exit();
		}

		_currentState = newState;
		_currentState.Enter();
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState?.PhysicsUpdate(delta);
	}
	
}
