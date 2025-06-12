using Godot;
using Microsoft.VisualBasic;
using System;

public partial class Enemy : Character
{
	public override void _Ready()
	{
		base._Ready();
		GD.Print(PathNode);
	}



}
