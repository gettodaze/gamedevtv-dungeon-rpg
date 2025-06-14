using Godot;
using System;

public abstract partial class CharacterState : Node
{
    public Character characterNode;
    protected abstract string AnimationString { get; }
    public virtual bool IsEligibleForRandom => true;

    public override void _Ready()
    {
        base._Ready();
        characterNode = GetOwner<Character>();
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public virtual void DisableState()
    {
        Log("DisableState");
        SetPhysicsProcess(false);

    }

    public virtual void EnableState()
    {
        SetPhysicsProcess(true);
        Log("EnableState");
        if (AnimationString != null) characterNode.AnimPlayerNode.Play(AnimationString);
    }

    public void Log(string msg)
    {
        characterNode.Log($"[{Name}] {msg}");
    }
}
