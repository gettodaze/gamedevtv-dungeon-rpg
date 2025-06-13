using Godot;
using System;

public partial class PlayerPostDashState : PlayerState
{
	[Export] private float duration = 0.5f;
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => null;
	Timer timerNode;


	public override void _Ready()
	{
		base._Ready();
		timerNode = AddTimer(duration, HandleTimeout);
	}

	private void HandleTimeout()
	{
		Log("PostDash timer stopped");
		playerNode.StateMachine.SwitchState<PlayerIdleState>();
	}

	public override void EnableState()
	{
		base.EnableState();
		playerNode.direction = Vector2.Zero;
		playerNode.AnimPlayerNode.Stop();
		timerNode.Start();
	}

	public override void DisableState()
	{
		base.DisableState();
		timerNode.Stop();
	}
}
