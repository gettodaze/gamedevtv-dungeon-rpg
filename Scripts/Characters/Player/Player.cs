using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float speed = 5.0f;
	[Export] public float dashSpeed = 10.0f;
	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer animPlayerNode;
	[Export] public Sprite3D sprite3DNode;
	[Export] public StateMachine stateMachine;
	public Vector2 direction = new();

	public override void _Ready()
	{
	}

	public void Move()
	{
		Velocity = new(direction.X, 0, direction.Y);
		Velocity *= speed;
		Flip();
		MoveAndSlide();
	}

	private void Flip()
	{
		if (direction.X < 0)
		{
			sprite3DNode.FlipH = true;
		}
		else if (direction.X > 0)
		{
			sprite3DNode.FlipH = false;
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);


	}
}
