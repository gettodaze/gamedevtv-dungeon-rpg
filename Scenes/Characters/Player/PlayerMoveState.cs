using Godot;
using System;

public partial class PlayerMoveState : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player characterNode = GetOwner<Player>();
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
