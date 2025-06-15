using Godot;
using System;

[GlobalClass]
public partial class HealthData : Resource
{
	[Signal]
	public delegate void HealthChangedEventHandler(int current, int max, int delta);

	[Export] public int MaxHealth = 100;
	[Export] public int CurrentHealth = 100;

	public void TakeDamage(int amount)
	{
		var delta = -Mathf.Min(CurrentHealth, amount);
		CurrentHealth += delta;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth, delta);
	}

	public void Heal(int amount)
	{
		var delta = Mathf.Min(MaxHealth - CurrentHealth, amount);
		CurrentHealth += delta;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth, delta);
	}
}
