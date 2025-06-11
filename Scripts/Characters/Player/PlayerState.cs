using Godot;
using System;

public abstract partial class PlayerState : Node
{
    protected Player characterNode;
    protected abstract string AnimationString { get; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        characterNode = GetOwner<Player>();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void DisableState()
    {
        GD.Print($"Disabling {AnimationString}");

    }

    public void EnableState()
    {
        GD.Print($"Enabling {AnimationString}");
        characterNode.animPlayerNode.Play(AnimationString);

    }
}
