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
		AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
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
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		if (direction == Vector2.Zero)
		{
			AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
		}
		else
		{
			AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
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
