using Godot;
using System;

public partial class EnemyIdleState : EnemyCanChaseState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;
    [Export] private float duration = 1.0f;
    Timer timerNode;
    public override void _Ready()
    {
        base._Ready();
        timerNode = AddTimer(duration, HandleTimeout);
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

    private void HandleTimeout()
    {
        Log("timer stopped");
        characterNode.StateMachine.SwitchStateRandom();

    }
}
