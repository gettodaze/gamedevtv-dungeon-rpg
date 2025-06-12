using Godot;
using System;

public partial class ReturnState : CharacterState
{
	protected override string AnimationString => GameConstants.ANIM_MOVE;

	public override void EnableState()
	{
		base.EnableState();
		var startPoint = characterNode.PathNode.Curve.GetPointPosition(0) + characterNode.PathNode.GlobalPosition;
		GD.Print("Point 0", startPoint);
		characterNode.GlobalPosition = startPoint;
	}

}
