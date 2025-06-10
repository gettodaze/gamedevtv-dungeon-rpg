using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float speed = 5.0f;
	private Vector2 direction = new();

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Velocity = new(direction.X, 0, direction.Y);
		Velocity *= speed;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		direction = Input.GetVector("MoveLeft", "MoveRight", "MoveForward", "MoveBackward");
	}
}
