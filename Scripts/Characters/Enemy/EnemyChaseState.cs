using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : CharacterState
{
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => GameConstants.ANIM_MOVE;
	[Export(PropertyHint.Range, "0,2,0.1")] private float calcMovementInterval = 0.5f;
	private CharacterBody3D target;
	private Timer calcMovementTimer;

	public override void _Ready()
	{
		base._Ready();
		calcMovementTimer = new();
		calcMovementTimer.WaitTime = calcMovementInterval;
		calcMovementTimer.Timeout += characterNode.RecalcFaceTarget;
		AddChild(calcMovementTimer);
	}

	public override void EnableState()
	{
		base.EnableState();
		target = characterNode.Area3DNode.GetOverlappingBodies().First() as CharacterBody3D;
		characterNode.Area3DNode.BodyExited += HandleBodyExit;
		calcMovementTimer.Start();
	}

	private void HandleBodyExit(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyIdleState>();
	}


	public override void DisableState()
	{
		base.DisableState();
		characterNode.Area3DNode.BodyExited -= HandleBodyExit;
		calcMovementTimer.Stop();

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		characterNode.NavigationAgentNode.TargetPosition = target.GlobalPosition;
		characterNode.MoveToDestination(recalc: false);
	}


}
