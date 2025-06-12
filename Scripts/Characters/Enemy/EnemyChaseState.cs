using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : CharacterState
{
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => GameConstants.ANIM_MOVE;
	private CharacterBody3D target;

	public override void EnableState()
	{
		base.EnableState();
		target = characterNode.Area3DNode.GetOverlappingBodies().First() as CharacterBody3D;
		characterNode.Area3DNode.BodyExited += HandleBodyExit;
	}

	private void HandleBodyExit(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyIdleState>();
	}


	public override void DisableState()
	{
		base.DisableState();
		characterNode.Area3DNode.BodyExited -= HandleBodyExit;

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		characterNode.NavigationAgentNode.TargetPosition = target.GlobalPosition;
		characterNode.MoveToDestination();
	}


}
