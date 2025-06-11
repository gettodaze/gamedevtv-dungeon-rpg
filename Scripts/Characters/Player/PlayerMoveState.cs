using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    public override void _Ready()
    {
        base._Ready();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }
}
