using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	private bool isReady = false;
	private CharacterState _currentState;
	[Export]
	public CharacterState CurrentState
	{
		get => _currentState;
		set
		{
			if (_currentState == value)
			{
				// _currentState.Log("StateMachine: skipping identity set");
				_currentState.Log("StateMachine: handling identity set");
				// return;
			};
			// GD.Print($"Setting state from {_currentState} to {value}");
			if (isReady) _currentState.DisableState();
			_currentState = value;
			if (isReady) value.EnableState();
		}
	}
	private CharacterState[] states;
	private CharacterState[] statesRandom;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		states = GetChildren().OfType<CharacterState>().ToArray();
		statesRandom = states
			.Where(state => state.IsEligibleForRandom)
			.ToArray();
		var stateString = string.Join(", ", states.Select(s => s.Name));
		// GD.Print($"{Name} ready with states ({stateString}). Enabling {CurrentState.characterNode.Name} {CurrentState.Name}");
		isReady = true;
		// CurrentState.EnableState();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SwitchState<T>()
	{
		CharacterState newState = null;
		foreach (CharacterState state in states)
		{
			if (state is T)
			{
				newState = state;
			}
		}
		if (newState == null)
		{
			GD.Print($"Could not find a valid state for {typeof(T)} in {states}");
			return;
		}

		CurrentState = newState;
	}
	public CharacterState SwitchStateRandom()
	{
		if (statesRandom == null || statesRandom.Length == 0)
		{
			GD.Print("No statesRandom available to switch to.");
			return null;
		}

		CurrentState = statesRandom[GD.Randi() % statesRandom.Length];
		return CurrentState;
	}
}
