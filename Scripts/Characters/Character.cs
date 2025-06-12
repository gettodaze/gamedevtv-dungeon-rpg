using Godot;
using System;

public partial class Character : CharacterBody3D
{
    [ExportGroup("Physics Settings")]
    [Export(PropertyHint.Range, "0,20,1")] private float speed = 5.0f;
    [Export(PropertyHint.Range, "0,50,1")] public float dashSpeed = 15.0f;
    [Export(PropertyHint.Range, "0,10,0.01")] public float gravity = 0.2f;
    [Export(PropertyHint.Range, "0,10,0.01")] public float jumpSpeed = 5f;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D Sprite3DNode { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D NavigationAgentNode { get; private set; }
    public Vector2 direction = new();
    public Vector3 destination = new();

    public override void _Ready()
    {
    }

    public void Jump()
    {
        GD.Print("Jump!");
        Velocity += Vector3.Up * jumpSpeed;
    }

    public void Move(bool dash = false, Action<KinematicCollision3D> onCollision = null)
    {
        var scaledDir = direction * (dash ? dashSpeed : speed);
        // float velY = IsOnFloor() ? 0 : Velocity.Y - 0.1f;
        Velocity = new(scaledDir.X, Velocity.Y, scaledDir.Y);
        if (!IsOnFloor()) Velocity += Vector3.Down * gravity;
        Flip();
        MoveAndSlide();


        int collisionCount = GetSlideCollisionCount();
        for (int i = 0; i < collisionCount; i++)
        {
            var collision = GetSlideCollision(i);
            onCollision?.Invoke(collision); // Call the callback if provided
        }
    }

    public void Flip()
    {
        if (direction.X < 0)
        {
            Sprite3DNode.FlipH = true;
        }
        else if (direction.X > 0)
        {
            Sprite3DNode.FlipH = false;
        }
    }

    public Vector3 GetPathIdxGlobalPosition(int i)
    {
        return PathNode.Curve.GetPointPosition(i) + PathNode.GlobalPosition;
    }
    public void MoveToDestination(Action atDestination)
    {
        if (GlobalPosition.DistanceTo(destination) < 0.1f) atDestination.Invoke();
        Velocity = GlobalPosition.DirectionTo(destination) * speed;
        Flip();
        MoveAndSlide();
    }



}
