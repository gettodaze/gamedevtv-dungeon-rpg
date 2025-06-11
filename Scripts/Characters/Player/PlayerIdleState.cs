using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
