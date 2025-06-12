using Godot;
using System;

public partial class EnemyMoveStateB : EnemyCanChaseState
{
    protected override string AnimationString => GameConstants.ANIM_MOVE;
}
