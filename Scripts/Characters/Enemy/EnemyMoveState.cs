using Godot;
using System;

public partial class EnemyMoveState : EnemyCanChaseState
{
    private TimerHelper timerNode;
    protected override string AnimationString => GameConstants.ANIM_MOVE;
    [Export] private float duration = 1.0f;
    public override void _Ready()
    {
        base._Ready();
        timerNode = new(this, HandleTimeout, duration, oneShot: false);

    }

    private void HandleTimeout()
    {
        Log("enemyMoveState timer stopped");
        characterNode.StateMachine.SwitchStateRandom();
        // if (characterNode.StateMachine.SwitchStateRandom() is EnemyMoveState)
        // {
        //     SetRandomDirection();
        //     timerNode.Start();
        // }
    }


    public override void DisableState()
    {
        base.DisableState();
        timerNode.Stop();

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        characterNode.Move(false, (collision) =>
        {
            // ignore if the collision is with the floor
            if (collision.GetNormal().Y < 0.7) SetRandomDirection();
        });

    }

    private void SetRandomDirection()
    {
        float angle = (float)(GD.Randf() * Mathf.Tau);
        characterNode.direction = new(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public override void EnableState()
    {
        base.EnableState();
        SetRandomDirection();
        timerNode.Start();
    }
}
