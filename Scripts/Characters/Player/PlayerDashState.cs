using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_DASH;
    [Export]
    private Timer timerNode;
    [Export] private float dashSpeed = 10;

    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += characterNode.stateMachine.SwitchState<PlayerPostDashState>;
    }

    public override void EnableState()
    {
        base.EnableState();
        GD.Print($"Enable Dash: Old Velocity {characterNode.Velocity}");
        timerNode.Start();

        // set dash velocity
        var dir = characterNode.direction;
        // note: maybe do this only when not already 0?
        // if (dir == Vector2.Zero)
        dir.X = characterNode.sprite3DNode.FlipH ? -1 : 1;
        dir.Y = 0;

        characterNode.Velocity = new(dir.X, 0, dir.Y);
        characterNode.Velocity *= dashSpeed;
        GD.Print($"Enable Dash: New Velocity {characterNode.Velocity}");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        characterNode.MoveAndSlide();

    }
}
