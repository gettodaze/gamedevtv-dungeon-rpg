using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_DASH;
    [Export] private float duration = 0.2f;
    private TimerHelper timerNode;

    public override void _Ready()
    {
        base._Ready();
        timerNode = new(this, playerNode.StateMachine.SwitchState<PlayerPostDashState>, duration, oneShot: true);
    }

    public override void EnableState()
    {
        base.EnableState();
        Log($"Enable Dash: Old Direction {playerNode.direction}");
        timerNode.Start();

        // set dash velocity
        var dir = playerNode.direction;
        // note: maybe do this only when not already 0?
        // if (dir == Vector2.Zero)
        // playerNode.direction.X = playerNode.Sprite3DNode.FlipH ? -1 : 1;
        playerNode.direction.Y = 0;

        // playerNode.Velocity = new(dir.X, 0, dir.Y);
        // playerNode.Velocity *= playerNode.dashSpeed;
        Log($"Enable Dash: New Direction {playerNode.direction}");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Log($"Processing dash physics with direction {playerNode.direction} and velocity {playerNode.Velocity}");
        playerNode.Move(true);
    }

}
