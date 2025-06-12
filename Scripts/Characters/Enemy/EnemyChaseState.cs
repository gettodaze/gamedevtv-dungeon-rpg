using Godot;
using System;

public partial class EnemyChaseState : CharacterState
{
	public override bool IsEligibleForRandom => false;
	protected override string AnimationString => GameConstants.ANIM_MOVE;


}
