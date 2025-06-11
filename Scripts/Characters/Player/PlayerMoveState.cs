using Godot;
using System;

public partial class PlayerMoveState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_MOVE;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (characterNode.direction == Vector2.Zero)
        {
            characterNode.StateMachine.SwitchState<PlayerIdleState>();
        }
        else characterNode.Move();

    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        characterNode.SetInputDirection();
        if (Input.IsActionJustPressed(GameConstants.INPUT_JUMP))
        {
            characterNode.Jump();
        }
        else if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.StateMachine.SwitchState<PlayerDashState>();
        }

    }
}
