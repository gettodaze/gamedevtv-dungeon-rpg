using Godot;
using System;

public partial class EnemyMoveStateA : CharacterState
{
    [Export] private Timer timerNode;
    protected override string AnimationString => GameConstants.ANIM_MOVE;
    public override void _Ready()
    {
        base._Ready();
        timerNode.Timeout += () =>
        {
            GD.Print("EnemyMove timer stopped");
            if (characterNode.StateMachine.SwitchStateRandom() is EnemyMoveStateA) EnableState();
        };
    }
    public override void DisableState()
    {
        base.DisableState();

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
