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
		characterNode.destination = startPoint;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.GlobalPosition.DistanceTo(characterNode.destination) < 0.1f) { GD.Print("Reached Destination"); characterNode.StateMachine.SwitchState<EnemyIdleStateB>(); };
		base._PhysicsProcess(delta);
		characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(characterNode.destination);
		characterNode.Flip();
		characterNode.MoveAndSlide();
	}

}
