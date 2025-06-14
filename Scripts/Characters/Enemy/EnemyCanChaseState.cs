using Godot;
using System;

public abstract partial class EnemyCanChaseState : CharacterState
{
	public override void EnableState()
	{
		base.EnableState();
		Log("Adding chase handler");
		characterNode.ChaseAreaNode.BodyEntered += EnemyHandleEnterArea3DNode;
		// needs to be done after we've finished enabling state
	}

	private void EnemyHandleEnterArea3DNode(Node3D body)
	{
		Log($"EnemyHandleEnterArea3DNode {body}");
		characterNode.StateMachine.SwitchState<EnemyChaseState>();

	}
	private void checkOverlappingBodies()
	{
		// Handle first overlapping body if any
		var overlappingBodies = characterNode.ChaseAreaNode.GetOverlappingBodies();
		if (overlappingBodies.Count > 0)
		{
			var firstBody = overlappingBodies[0];
			if (firstBody != null)
			{
				CallDeferred(nameof(EnemyHandleEnterArea3DNode), firstBody);
			}
		}
	}

	public override void DisableState()
	{
		base.DisableState();
		Log("Removing chase handler");
		characterNode.ChaseAreaNode.BodyEntered -= EnemyHandleEnterArea3DNode;

	}


}
