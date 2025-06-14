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
    [Export] public Node3D VisualRootNode { get; private set; }
    [Export] public StateMachine StateMachine { get; private set; }
    [Export] public Area3D HitBoxNode { get; private set; }
    [Export] public Area3D HurtBoxNode { get; private set; }
    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D NavigationAgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }
    private int _health = 50;
    [Export]
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0) StateMachine.SwitchState<DeathState>();
        }
    }
    [Export] public int attackStrength = 5;


    public Vector2 direction = new();

    public override void _Ready()
    {
        base._Ready();
        StateMachine.CurrentState.EnableState();
        if (NavigationAgentNode == null) return;
        NavigationAgentNode.NavigationFinished += () => Log($"navigation finished.");
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
        attackTarget.TakeDamage(damage ?? attackStrength);
    }

    public void TakeDamage(int damage)
    {
        CallDeferred(nameof(_TakeDamage), damage);
    }

    private void ShowDamage(float damage)
    {
        var label = new Label3D
        {
            Text = damage.ToString(),
            // Billboard = BaseMaterial3D.BillboardModeEnum.Enabled,
            Modulate = new Color(1, 0, 0), // red text
            FontSize = 30
        };

        AddChild(label); // or GetParent().AddChild(label) to put it outside your node
        label.GlobalPosition = GlobalPosition + Vector3.Up; // float above head
        // Random horizontal offset to make it look dynamic:
        label.GlobalPosition += new Vector3((float)GD.RandRange(-0.5, 0.5), 0, 0);

        // Animate upward and fade out
        var tween = CreateTween();
        tween.TweenProperty(label, "global_position:y", label.GlobalPosition.Y + 1.0f, 0.6f);
        tween.TweenProperty(label, "modulate:a", 0.0f, 0.6f);
        tween.TweenCallback(Callable.From(label.QueueFree));
    }


    public async void _TakeDamage(int damage)
    {
        Sprite3DNode.Modulate = new Color(1, 0, 0);  // red flash
        Log($"HIT {Name} takes {damage} damage. Current health: {Health}");
        Health -= damage;
        ShowDamage(damage);
        await ToSignal(GetTree().CreateTimer(0.1f), "timeout");

        Sprite3DNode.Modulate = new Color(1, 1, 1);  // restore color
    }

}

