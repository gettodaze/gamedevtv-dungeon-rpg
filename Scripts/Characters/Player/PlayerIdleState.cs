using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;
}
