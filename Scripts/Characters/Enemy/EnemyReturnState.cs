using Godot;
using System;

public partial class EnemyReturnState : CharacterState
{
	protected override string AnimationString => GameConstants.ANIM_MOVE;

	public override void EnableState()
	{
		base.EnableState();
		characterNode.NavigationAgentNode.TargetPosition = characterNode.GetPathIdxGlobalPosition(0);
		characterNode.NavigationAgentNode.NavigationFinished += characterNode.StateMachine.SwitchState<EnemyPatrolState>;

	}

	public override void DisableState()
	{
		base.DisableState();
		characterNode.NavigationAgentNode.NavigationFinished -= characterNode.StateMachine.SwitchState<EnemyPatrolState>;

	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		characterNode.MoveToDestination();
	}
}
