using Godot;
using System;

[GlobalClass]
public partial class HealthData : Resource
{
	[Signal]
	public delegate void HealthChangedEventHandler(int current, int max);

	[Export] public int MaxHealth = 100;
	[Export] public int CurrentHealth = 100;

	public void TakeDamage(int amount)
	{
		CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}

	public void Heal(int amount)
	{
		CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth);
	}
}
