using Godot;
using System;

public partial class EnemyMoveState : EnemyCanChaseState
{
    [Export] private Timer timerNode;
    protected override string AnimationString => GameConstants.ANIM_MOVE;
    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += () =>
        {
            Log("enemyMoveState timer stopped");
            if (characterNode.StateMachine.SwitchStateRandom() is EnemyMoveState)
            {
                SetRandomDirection();
                timerNode.Start();
            }
        };
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
