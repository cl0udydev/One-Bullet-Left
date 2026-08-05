using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyStateMachine : Node
{
	private EnemyState _currentState;
	private Dictionary<string, EnemyState> _states = new();

	public void SetupStates(EnemyController controller, MovementComponent movement)
	{
		_states.Add("idle", new EnemyIdleState(controller, this, movement));
		_states.Add("chase", new EnemyChaseState(controller, this, movement));
		_states.Add("attack", new EnemyAttackState(controller, this, movement));

		_currentState = _states["idle"];
    	_currentState.Enter();
	}
	
	public void ChangeStateByName(string stateName)
	{
		if (!_states.ContainsKey(stateName)) return;
		
		_currentState?.Exit();
		_currentState = _states[stateName];
		_currentState.Enter();
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState?.PhysicsUpdate(delta);
	}
	
}
