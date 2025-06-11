using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.NOTIFICATION_ENABLE_STATE)
        {
            characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        }
    }
}
