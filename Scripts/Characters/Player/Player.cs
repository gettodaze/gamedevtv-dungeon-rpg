using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float speed = 5.0f;
	[Export] private AnimationPlayer AnimPlayerNode;
	[Export] private Sprite3D Sprite3DNode;
	private Vector2 direction = new();

	public override void _Ready()
	{
		base._Ready();
		AnimPlayerNode.Play("Idle");
	}

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
		if (direction == Vector2.Zero)
		{
			AnimPlayerNode.Play("Idle");
		}
		else
		{
			AnimPlayerNode.Play("Move");
		}
		if (direction.X < 0)
		{
			Sprite3DNode.FlipH = true;
		}
		else if (direction.X > 0)
		{
			Sprite3DNode.FlipH = false;
		}
	}
}
