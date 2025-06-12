using Godot;
using System;

public abstract partial class CharacterState : Node
{
    protected Character characterNode;
    protected abstract string AnimationString { get; }
    public override void _Ready()
    {
        base._Ready();
        characterNode = GetOwner<Character>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public virtual void DisableState()
    {
        GD.Print($"{characterNode.Name}: Disabling {AnimationString}");
        SetPhysicsProcess(false);

    }

    public virtual void EnableState()
    {
        SetPhysicsProcess(true);
        GD.Print($"{characterNode.Name}: Enabling {AnimationString}");
        characterNode.AnimPlayerNode.Play(AnimationString);
    }
}
