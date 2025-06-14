using Godot;
using System;

public partial class Main : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetTreePaused(true);
	}

	public void SetTreePaused(bool value)
	{
		GetTree().Paused = value;
	}
}
