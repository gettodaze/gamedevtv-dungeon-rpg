using Godot;
using System;

public partial class EnemyIdleState : CharacterState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;

}
