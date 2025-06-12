using Godot;
using System;

public partial class EnemyIdleStateA : CharacterState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;
    [Export] private Timer timerNode;
    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += () => GD.Print("EnemyIdle timer stopped");
        timerNode.Timeout += () => characterNode.StateMachine.SwitchStateRandom();
    }

    public override void EnableState()
    {
        base.EnableState();
        timerNode.Start();
    }
}
