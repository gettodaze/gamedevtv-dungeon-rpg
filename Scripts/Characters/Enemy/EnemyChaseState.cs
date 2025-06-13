using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : CharacterState
{
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => GameConstants.ANIM_MOVE;
	[Export(PropertyHint.Range, "0,2,0.1")] private float calcMovementInterval = 0.5f;
	private CharacterBody3D target;
	private TimerHelper calcMovementTimer;

	public override void _Ready()
	{
		base._Ready();
		calcMovementTimer = new(this, characterNode.RecalcFaceTarget, calcMovementInterval);
	}

	public override void EnableState()
	{
		base.EnableState();
		target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
		characterNode.ChaseAreaNode.BodyExited += HandleChaseBodyExit;
		characterNode.AttackAreaNode.BodyEntered += HandleAttackBodyEntered;
		calcMovementTimer.Start();
	}

	private void HandleAttackBodyEntered(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyAttackState>();
	}

	private void HandleChaseBodyExit(Node3D body)
	{
		characterNode.StateMachine.SwitchState<EnemyIdleState>();
	}


	public override void DisableState()
	{
		base.DisableState();
		characterNode.ChaseAreaNode.BodyExited -= HandleChaseBodyExit;
		characterNode.AttackAreaNode.BodyEntered -= HandleAttackBodyEntered;
		calcMovementTimer.Stop();

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		characterNode.NavigationAgentNode.TargetPosition = target.GlobalPosition;
		characterNode.MoveToDestination(recalc: false);
	}


}
