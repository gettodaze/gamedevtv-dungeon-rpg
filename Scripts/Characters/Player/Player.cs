using Godot;
using System;

public partial class Player : CharacterNode
{
	[Export(PropertyHint.Range, "0,20,1")] private float speed = 5.0f;
	[Export(PropertyHint.Range, "0,50,1")] public float dashSpeed = 15.0f;
	[Export(PropertyHint.Range, "0,10,0.01")] public float gravity = 0.2f;
	[Export(PropertyHint.Range, "0,10,0.01")] public float jumpSpeed = 5f;

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
		var scaledDir = direction * (dash ? dashSpeed : speed);
		// float velY = IsOnFloor() ? 0 : Velocity.Y - 0.1f;
		Velocity = new(scaledDir.X, Velocity.Y, scaledDir.Y);
		if (!IsOnFloor()) Velocity += Vector3.Down * gravity;
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

	public void SetInputDirection()
	{
		var oldDir = direction;
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		GD.Print($"Set character direction from {oldDir} to {direction}");
	}

}
