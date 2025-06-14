using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_MOVE;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed(GameConstants.INPUT_JUMP) & playerNode.IsOnFloor())
        {
            playerNode.Jump();
        }
        if (playerNode.direction == Vector2.Zero)
        {
            playerNode.StateMachine.SwitchState<PlayerIdleState>();
        }
        else playerNode.Move();

    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        playerNode.SetInputDirection();
        if (playerNode.CheckAndHandleAttackInput()) return;

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            playerNode.StateMachine.SwitchState<PlayerDashState>();
        }

    }
}
