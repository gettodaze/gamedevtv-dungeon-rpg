using Godot;
using System;

public partial class EnemyIdleState : EnemyCanChaseState
{
    protected override string AnimationString => GameConstants.ANIM_IDLE;
    [Export] private float duration = 1.0f;
    TimerHelper timerNode;
    public override void _Ready()
    {
        base._Ready();
        timerNode = new(this, HandleTimeout, duration, oneShot: false);
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
        // JTODO: maybe its the switch state random thats still giving me problems
        Log("timer stopped");
        characterNode.StateMachine.SwitchStateRandom();

    }
}
