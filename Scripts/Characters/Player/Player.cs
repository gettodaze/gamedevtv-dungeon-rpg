using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export(PropertyHint.Range, "0,20,1")] private float speed = 5.0f;
	[Export(PropertyHint.Range, "0,50,1")] public float dashSpeed = 15.0f;
	[Export(PropertyHint.Range, "0,10,0.1")] public float jumpSpeed = 10.0f;
	[ExportGroup("Required Nodes")]
	[Export] public AnimationPlayer AnimPlayerNode { get; private set; }
	[Export] public Sprite3D Sprite3DNode { get; private set; }
	[Export] public StateMachine StateMachine { get; private set; }
	public Vector2 Direction
	{
		get => new(Velocity.X, Velocity.Z);
		set => Velocity = new(value.X, Velocity.Y, value.Y);
	}

	public override void _Ready()
	{
	}

	public void Jump()
	{
		GD.Print("Jump!");
		Velocity += Vector3.Up * jumpSpeed;
	}

	public void Move(bool dash = false)
	{
		GD.Print($"Moved: OG velocity {Velocity}");
		var scaledDir = direction * (dash ? dashSpeed : speed);
		// float velY = IsOnFloor() ? 0 : Velocity.Y - 0.1f;
		Velocity = new(scaledDir.X, Velocity.Y - 0.1f, scaledDir.Y);
		Flip();
		MoveAndSlide();
		GD.Print($"Moved with velocity {Velocity}");
		var collision = GetLastSlideCollision();
		if (collision != null)
		{
			GD.Print(collision);
		}
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

	public void SetInputDirection()
	{
		var oldDir = direction;
		Direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		GD.Print($"Set character direction from {oldDir} to {direction}");
	}

}
