using Godot;
using System;

public abstract partial class PlayerState : CharacterState<Player>
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        SetProcessInput(false);
    }

    public override void DisableState()
    {
        base.EnableState();
        SetProcessInput(false);

    }

    public override void EnableState()
    {
        base.EnableState();
        SetProcessInput(true);
    }
}
