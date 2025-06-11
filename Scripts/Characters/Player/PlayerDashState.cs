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
        timerNode.Timeout += characterNode.StateMachine.SwitchState<PlayerPostDashState>;
    }

    public override void EnableState()
    {
        base.EnableState();
        GD.Print($"Enable Dash: Old Direction {characterNode.direction}");
        timerNode.Start();

        // set dash velocity
        var dir = characterNode.direction;
        // note: maybe do this only when not already 0?
        // if (dir == Vector2.Zero)
        dir.X = characterNode.Sprite3DNode.FlipH ? -1 : 1;
        dir.Y = 0;

        // characterNode.Velocity = new(dir.X, 0, dir.Y);
        // characterNode.Velocity *= characterNode.dashSpeed;
        GD.Print($"Enable Dash: New Direction {characterNode.direction}");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        characterNode.MoveAndSlide();

    }
}
