using Godot;
using System;

public partial class PlayerPostDashState : PlayerState
{
	[Export]
	private Timer timerNode;
	protected override string AnimationString => null;

	public override void _Ready()
	{
		base._Ready();
		timerNode.Timeout += () => GD.Print("PostDash timer stopped");
		timerNode.Timeout += characterNode.stateMachine.SwitchState<PlayerIdleState>;
	}
	public override void EnableState()
	{
		GD.Print("Enabling PostDashState");
		characterNode.animPlayerNode.Stop();
		timerNode.Start();
	}
}
