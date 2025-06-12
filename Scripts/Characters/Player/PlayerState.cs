using Godot;
using System;

public abstract partial class PlayerState : CharacterState
{
    protected Player playerNode;
    public override void _Ready()
    {
        base._Ready();
        playerNode = characterNode as Player;
    }

    public override void DisableState()
    {
        base.DisableState();
        SetProcessInput(false);

    }

    public override void EnableState()
    {
        base.EnableState();
        SetProcessInput(true);
    }
}
