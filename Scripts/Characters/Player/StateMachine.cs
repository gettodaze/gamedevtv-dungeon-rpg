using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerState[] states = GetChildren().OfType<PlayerState>().ToArray();
		GD.Print(string.Join(", ", states.Select(s => s.Name)));

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
