using Godot;
using System;

public partial class EnemyIdleStateB : EnemyCanChaseState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;

}
