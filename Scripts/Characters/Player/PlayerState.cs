using Godot;
using System;

public abstract partial class PlayerState : CharacterState
{
    protected Player characterNode;
    protected abstract string AnimationString { get; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void DisableState()
    {
        GD.Print($"Disabling {AnimationString}");
        SetPhysicsProcess(false);
        SetProcessInput(false);

    }

    public override void EnableState()
    {
        GD.Print($"Enabling {AnimationString}");
        characterNode.AnimPlayerNode.Play(AnimationString);
        SetPhysicsProcess(true);
        SetProcessInput(true);
    }
}
