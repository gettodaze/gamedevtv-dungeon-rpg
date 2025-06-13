using Godot;
using System;

public partial class PlayerPostDashState : PlayerState
{
	[Export] private Timer timerNode;
	protected override string AnimationString => null;

	public override void _Ready()
	{
		base._Ready();
		timerNode.Timeout += () => Log("PostDash timer stopped");
		timerNode.Timeout += playerNode.StateMachine.SwitchState<PlayerIdleState>;
	}
	public override void EnableState()
	{
		base.EnableState();
		playerNode.direction = Vector2.Zero;
		playerNode.AnimPlayerNode.Stop();
		timerNode.Start();
	}
}
