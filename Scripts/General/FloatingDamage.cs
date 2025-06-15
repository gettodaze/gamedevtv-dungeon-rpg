using Godot;
using System;

public partial class FloatingDamage : Node3D
{
	[Export] public Label3D LabelNode { get; private set; }

	[Export] public float FloatAmount { get; private set; } = 0.5f;
	[Export] public float Duration { get; private set; } = 0.6f;
	[Export] public float ScaleFactor { get; private set; } = 1.0f;

	public void Initialize(int delta, bool healed)
	{
		Color color = healed ? new Color(0, 1, 0) : new Color(1, 0, 0);
		LabelNode.Text = delta.ToString();
		LabelNode.Modulate = color;
		LabelNode.FontSize = GetScaledFontSize(LabelNode.FontSize, delta);

		var tween = CreateTween();
		tween.TweenProperty(this, "global_position:y", GlobalPosition.Y + FloatAmount, Duration);
		tween.TweenProperty(LabelNode, "modulate:a", 0.0f, Duration);
		tween.TweenCallback(Callable.From(QueueFree));
	}

	private int GetScaledFontSize(int originalFontSize, int delta)
	{
		float multiplier = MathF.Max(1.0f, delta / ScaleFactor);
		int newFontSize = (int)(originalFontSize * multiplier);
		if (newFontSize < 1)
			throw new InvalidOperationException($"newFontSize cannot be less than 1: {newFontSize}");

		GD.Print($"[Floating Font] Scaled for delta {delta}: {originalFontSize}*{multiplier}={newFontSize}");
		return newFontSize;
	}



}
