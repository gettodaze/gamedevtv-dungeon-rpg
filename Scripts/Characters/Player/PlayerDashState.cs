using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_DASH;
    [Export]
    private Timer timerNode;

    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += characterNode.stateMachine.SwitchState<PlayerIdleState>;
    }

    public override void EnableState()
    {
        base.EnableState();
        timerNode.Start();
    }
}
