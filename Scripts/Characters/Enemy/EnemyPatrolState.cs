using Godot;
using System;

public partial class EnemyPatrolState : CharacterState
{
	protected override string AnimationString => GameConstants.ANIM_MOVE;
	private int pathIdx = 0;
	public override void EnableState()
	{
		base.EnableState();
		pathIdx = 0;
		incrementPathIdx();
		characterNode.NavigationAgentNode.NavigationFinished += incrementPathIdx;
	}

	public override void DisableState()
	{
		base.DisableState();
		characterNode.NavigationAgentNode.NavigationFinished -= incrementPathIdx;

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		characterNode.MoveToDestination();

	}

	public void incrementPathIdx()
	{
		pathIdx = (pathIdx + 1) % characterNode.PathNode.Curve.PointCount;
		characterNode.NavigationAgentNode.TargetPosition = characterNode.GetPathIdxGlobalPosition(pathIdx);
	}
}
