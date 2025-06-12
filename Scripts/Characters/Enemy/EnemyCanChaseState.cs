using Godot;
using System;

public abstract partial class EnemyCanChaseState : CharacterState
{
	public override void EnableState()
	{
		base.EnableState();
		GD.Print($"{Name}: Adding chase handler");
		characterNode.Area3DNode.BodyEntered += EnemyHandleEnterArea3DNode;
	}

	private void EnemyHandleEnterArea3DNode(Node3D body)
	{
		GD.Print($"EnemyHandleEnterArea3DNode {Name}, {body}");
		characterNode.StateMachine.SwitchState<EnemyChaseState>();

	}

	public override void DisableState()
	{
		base.DisableState();
		GD.Print($"{Name}: Removing chase handler");
		characterNode.Area3DNode.BodyEntered -= EnemyHandleEnterArea3DNode;

	}


}
