using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_MOVE;
}
