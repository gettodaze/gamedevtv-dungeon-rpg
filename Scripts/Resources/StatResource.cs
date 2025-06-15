using Godot;
using System;

[GlobalClass]
public partial class StatResource : Resource
{
	[Signal]
	public delegate void HealthChangedEventHandler(int current, int max, int delta);
	[Signal]
	public delegate void AttackChangedEventHandler(int attack);

	[Export] public int MaxHealth = 100;
	[Export] public int CurrentHealth = 100;
	private int _attackStrength;
	[Export]
	public int AttackStrength
	{
		get => _attackStrength;
		set
		{
			_attackStrength = value;
			EmitSignal(SignalName.AttackChanged, _attackStrength);

		}
	}

	public void TakeDamage(int amount)
	{
		GD.Print($"TakeDamage {amount} from ({CurrentHealth}, {MaxHealth})");
		var delta = -Mathf.Min(CurrentHealth, amount);
		CurrentHealth += delta;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth, delta);
	}

	public void Heal(int amount)
	{
		GD.Print($"Heal {amount} from ({CurrentHealth}, {MaxHealth})");
		var delta = Mathf.Min(MaxHealth - CurrentHealth, amount);
		CurrentHealth += delta;
		EmitSignal(SignalName.HealthChanged, CurrentHealth, MaxHealth, delta);
	}

	public void Log(object msg)
	{
		GD.Print($"StatResource: {msg}");
	}
}
