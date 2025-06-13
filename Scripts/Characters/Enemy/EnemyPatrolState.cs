using Godot;
using System;

public partial class EnemyPatrolState : EnemyCanChaseState
{
	protected override string AnimationString => GameConstants.ANIM_MOVE;
	private int pathIdx = 0;
	private TimerHelper idleTimer;
	[Export(PropertyHint.Range, "0,20,0.1")] private float maxIdleTime = 4;
	private bool processMove = true;
	public override bool IsEligibleForRandom => false;
	public override void _Ready()
	{
		base._Ready();
		idleTimer = new(this, HandleIdleTimeout, oneShot: true);
	}

	private void HandleIdleTimeout()
	{
		processMove = true;
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
	}

	public override void EnableState()
	{
		base.EnableState();
		pathIdx = 0;
		handleReachedPathVertex();
		characterNode.NavigationAgentNode.NavigationFinished += handleReachedPathVertex;
	}

	public override void DisableState()
	{
		base.DisableState();
		characterNode.NavigationAgentNode.NavigationFinished -= handleReachedPathVertex;

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (processMove) characterNode.MoveToDestination();

	}

	public void handleReachedPathVertex()
	{
		processMove = false;
		characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
		RandomNumberGenerator rng = new();
		idleTimer.Start(rng.RandfRange(0, maxIdleTime));
		pathIdx = (pathIdx + 1) % characterNode.PathNode.Curve.PointCount;
		characterNode.NavigationAgentNode.TargetPosition = characterNode.GetPathIdxGlobalPosition(pathIdx);

	}
}
