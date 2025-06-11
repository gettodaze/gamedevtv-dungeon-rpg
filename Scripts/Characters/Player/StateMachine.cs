using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	private bool isReady = false;
	private PlayerState _currentState;
	[Export]
	private PlayerState CurrentState
	{
		get => _currentState;
		set
		{
			GD.Print($"Setting state from {_currentState} to {value}");
			_currentState = value;
			if (isReady) value.EnableState();
		}
	}
	private PlayerState[] states;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		states = GetChildren().OfType<PlayerState>().ToArray();
		GD.Print(string.Join(", ", states.Select(s => s.Name)));
		isReady = true;
		CurrentState.EnableState();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
