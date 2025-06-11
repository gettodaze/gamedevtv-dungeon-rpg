using Godot;
using System;

public partial class CharacterNode : CharacterBody3D
{
	[Export] public float speed = 5.0f;

	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimPlayerNode { get; private set; }
	[Export] public Sprite3D Sprite3DNode { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }

	public Vector2 direction = new();
}
