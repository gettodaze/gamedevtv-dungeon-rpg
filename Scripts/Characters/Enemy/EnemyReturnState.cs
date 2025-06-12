using Godot;
using System;

public partial class EnemyReturnState : CharacterState
{
	protected override string AnimationString => GameConstants.ANIM_MOVE;

	public override void EnableState()
	{
		base.EnableState();
		characterNode.destination = characterNode.GetPathIdxGlobalPosition(0);
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.MoveToDestination(() => { GD.Print("Reached Destination"); characterNode.StateMachine.SwitchState<EnemyPatrolState>(); });
	}

}
