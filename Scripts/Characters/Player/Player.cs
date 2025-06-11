using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export(PropertyHint.Range, "0,20,1")] private float speed = 5.0f;
	[Export(PropertyHint.Range, "0,50,1")] public float dashSpeed = 15.0f;
	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimPlayerNode { get; private set; }
	[Export] public Sprite3D Sprite3DNode { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }
	public Vector2 direction = new();

	public override void _Ready()
	{
	}

	public void Move(bool dash = false)
	{
		Velocity = new(direction.X, 0, direction.Y);
		Velocity *= dash ? dashSpeed : speed;
		Flip();
		MoveAndSlide();
	}

	private void Flip()
	{
		if (direction.X < 0)
		{
			Sprite3DNode.FlipH = true;
		}
		else if (direction.X > 0)
		{
			Sprite3DNode.FlipH = false;
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);


	}
}
