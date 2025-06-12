using Godot;
using System;

public abstract partial class EnemyCanChaseState : CharacterState
{
	public override void EnableState()
	{
		base.EnableState();
		GD.Print($"{Name}: Adding chase handler");
		characterNode.Area3DNode.AreaEntered += EnemyHandleEnterArea3DNode;
	}

	public override void DisableState()
	{
		base.DisableState();
		GD.Print($"{Name}: Removing chase handler");
		characterNode.Area3DNode.AreaEntered -= EnemyHandleEnterArea3DNode;

	}

	private void EnemyHandleEnterArea3DNode(Area3D area)
	{
		GD.Print($"EnemyHandleEnterArea3DNode {Name}, {area}");
	}
}
