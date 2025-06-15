using Godot;
using System;

public partial class StatsContainer : UIContainer
{
	// Called when the node enters the scene tree for the first time.
	[Export] public StatResource Health;
	[Export] public TextureProgressBar HealthBar;
	[Export] public Label StrengthLabel;
	[Export] public Label EnemyLabel;

	public override void _Ready()
	{
		Health.HealthChanged += OnHealthChanged;
		// Trigger initial update in case it's already damaged
		OnHealthChanged(Health.CurrentHealth, Health.MaxHealth, 0);
		StrengthLabel.Text = Health.AttackStrength.ToString();
		GameEvents.OnNewEnemyCount += HandleNewEnemyCount;

	}

	private void HandleNewEnemyCount(int count)
	{
		EnemyLabel.Text = count.ToString();
	}

	private void OnHealthChanged(int current, int max, int delta)
	{
		HealthBar.MaxValue = max;
		HealthBar.Value = current;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public override void _ExitTree()
	{
		Health.HealthChanged -= OnHealthChanged;
		GameEvents.OnNewEnemyCount -= HandleNewEnemyCount;
	}
}
