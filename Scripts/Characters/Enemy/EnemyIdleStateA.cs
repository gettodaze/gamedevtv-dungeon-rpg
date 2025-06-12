using Godot;
using System;

public partial class EnemyIdleStateA : EnemyCanChaseState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;
    [Export] private Timer timerNode;
    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += () => GD.Print($"{Name} EnemyIdle timer stopped");
        timerNode.Timeout += () => characterNode.StateMachine.SwitchStateRandom();
    }

    public override void EnableState()
    {
        base.EnableState();
        timerNode.Start();
    }
    public override void DisableState()
    {
        base.DisableState();
        timerNode.Stop();

    }
}
