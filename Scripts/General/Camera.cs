using Godot;
using System;

public partial class Camera : Camera3D
{
	private Node ogTarget;
	[Export] private Node target;
	[Export] private Vector3 positionFromTarget;
	public override void _Ready()
	{
		base._Ready();
		ogTarget = GetParent();
		GameEvents.OnStartGame += HandleStartGame;
		GameEvents.OnDefeat += HandleDefeat;
	}

	private void HandleDefeat()
	{
		// var curGlobal = GlobalPosition;
		Reparent(ogTarget);
	}

	private void HandleStartGame()
	{
		Reparent(target);
		Position = positionFromTarget;
	}

}
