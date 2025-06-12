using Godot;
using System;

public abstract partial class CharacterState : Node
{
    public Character characterNode;
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
        GD.Print($"{characterNode.Name}: Disabling {Name}");
        SetPhysicsProcess(false);

    }

    public virtual void EnableState()
    {
        SetPhysicsProcess(true);
        GD.Print($"{characterNode.Name}: Enabling {Name}");
        characterNode.AnimPlayerNode.Play(AnimationString);
    }
}
