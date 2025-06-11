using Godot;
using System;

public partial class IdleState : Node
{
	protected Enemy characterNode;
	protected string AnimationString => GameConstants.ANIM_IDLE;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		characterNode = GetOwner<Enemy>();
		SetPhysicsProcess(false);
	}

	public virtual void DisableState()
	{
		GD.Print($"Disabling {AnimationString}");
		SetPhysicsProcess(false);

	}

	public virtual void EnableState()
	{
		GD.Print($"Enabling {AnimationString}");
		characterNode.AnimPlayerNode.Play(AnimationString);
		SetPhysicsProcess(true);
	}


}