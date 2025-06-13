using Godot;
using System;

public partial class Player : Character
{

	public void SetInputDirection()
	{
		// var oldDir = direction;
		direction = Input.GetVector(GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FORWARD, GameConstants.INPUT_MOVE_BACKWARD);
		// Log($"Set character direction from {oldDir} to {direction}");
	}
}
