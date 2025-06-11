using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (characterNode.direction != Vector2.Zero)
        {
            characterNode.stateMachine.SwitchState<PlayerMoveState>();
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            characterNode.stateMachine.SwitchState<PlayerDashState>();
        }

    }
}
