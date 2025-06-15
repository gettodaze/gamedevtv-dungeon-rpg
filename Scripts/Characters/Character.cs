using Godot;
using System;

public abstract partial class Character : CharacterBody3D
{
    [ExportGroup("Physics Settings")]
    [Export(PropertyHint.Range, "0,20,1")] private float speed = 5.0f;
    [Export(PropertyHint.Range, "0,50,1")] public float dashSpeed = 15.0f;
    [Export(PropertyHint.Range, "0,10,0.01")] public float gravity = 0.2f;
    [Export(PropertyHint.Range, "0,10,0.01")] public float jumpSpeed = 5f;
    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D Sprite3DNode { get; private set; }
    [Export] public Node3D VisualRootNode { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D HitBoxNode { get; private set; }
    [Export] public Area3D HurtBoxNode { get; private set; }
    [Export] public StatResource Stats { get; private set; }
    [Export] public PackedScene DamageLabelScene { get; private set; }


    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D NavigationAgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }
    public bool Dead { get; private set; } = false;



    public Vector2 direction = new();

    public override void _Ready()
    {
        base._Ready();
        Stats.EmitSignal(StatResource.SignalName.HealthChanged, Stats.CurrentHealth, Stats.MaxHealth, 0);
        StateMachine.CurrentState.EnableState();
        Stats.HealthChanged += HandleHealthChanged;
        if (NavigationAgentNode != null)
            NavigationAgentNode.NavigationFinished += () => Log($"navigation finished.");
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        Stats.HealthChanged -= HandleHealthChanged;

    }



    public void Jump()
    {
        Log("Jump!");
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
            VisualRootNode.Scale = new Vector3(-1, 1, 1);
        }
        else if (direction.X > 0)
        {
            VisualRootNode.Scale = new Vector3(1, 1, 1);
        }
    }

    public Vector3 GetPathIdxGlobalPosition(int i)
    {
        return PathNode.Curve.GetPointPosition(i) + PathNode.GlobalPosition;
    }
    public void MoveToDestination(bool recalc = true)
    {
        if (recalc) RecalcFaceTarget();
        Move();
    }
    public void RecalcFaceTarget()
    {
        // var oldDir = direction;
        var vel = GlobalPosition.DirectionTo(NavigationAgentNode.GetNextPathPosition());
        // Velocity = GlobalPosition.DirectionTo(NavigationAgentNode.TargetPosition);
        direction = new(vel.X, vel.Z);
        // Log($"Recalculated MoveToDestination from {oldDir} to {direction}");
    }

    public void Log(object msg)
    {
        GD.Print($"{Name}: {msg}");
    }

    internal void Hit(Node3D body, int? damage = null)
    {
        var attackTarget = body.GetOwnerOrNull<Character>();
        if (attackTarget == null)
        {
            Log($"HIT non-character {body.Name}");
            return;
        }
        attackTarget.Stats.TakeDamage(damage ?? Stats.AttackStrength);
    }

    private async void HandleHealthChanged(int current, int max, int delta)
    {
        if (delta == 0) return;
        Log($"HandleHealthChanged(delta={delta})");
        if (!Dead && current == 0)
        {
            StateMachine.SwitchState<DeathState>();
        }
        var healed = delta > 0;
        delta = Math.Abs(delta);
        Color color = healed ? new Color(0, 1, 0) : new Color(1, 0, 0); // green/red

        var label = DamageLabelScene.Instantiate<FloatingDamage>();
        AddChild(label); // or GetTree().Root.AddChild(label) to float independently
        label.GlobalPosition = GlobalPosition + Vector3.Up + new Vector3((float)GD.RandRange(-0.5, 0.5), 0, 0);
        label.Initialize(delta, healed);

        Sprite3DNode.Modulate = color;  // color flash
        await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        Sprite3DNode.Modulate = new Color(1, 1, 1);  // restore color
    }

    public abstract void HandleDeath();
}

