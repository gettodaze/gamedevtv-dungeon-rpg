using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionPressed(GameConstants.INPUT_JUMP) & characterNode.IsOnFloor())
        {
            characterNode.Jump();
        }
        characterNode.Move();
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        characterNode.SetInputDirection();

        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachine.SwitchState<PlayerDashState>();
        }

    }
}
