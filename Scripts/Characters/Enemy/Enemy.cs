using Godot;
using Microsoft.VisualBasic;
using System;

public partial class Enemy : Character
{
	public override void _Ready()
	{
		base._Ready();
		Log(PathNode);
	}

	public override void HandleDeath()
	{
		if (PathNode != null) PathNode.QueueFree();
		else QueueFree();
	}



}
