using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed(GameConstants.INPUT_JUMP) & playerNode.IsOnFloor())
        {
            playerNode.Jump();
        }
        playerNode.Move();
        if (playerNode.direction != Vector2.Zero)
        {
            playerNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        playerNode.SetInputDirection();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.StateMachine.SwitchState<PlayerDashState>();
        }

    }
}
