using Godot;
using System;

public abstract partial class CharacterState<T> : Node where T : CharacterNode
{
    protected T characterNode;
    protected abstract string AnimationString { get; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        characterNode = GetOwner<T>();
        SetPhysicsProcess(false);
    }

    public virtual void DisableState()
    {
        GD.Print($"Disabling {AnimationString}");
        SetPhysicsProcess(false);

    }

    public virtual void EnableState()
    {
        GD.Print($"Enabling {AnimationString}");
        characterNode.AnimPlayerNode.Play(AnimationString);
        SetPhysicsProcess(true);
    }
}
