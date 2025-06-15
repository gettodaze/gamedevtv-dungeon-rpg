using Godot;
using System;

public partial class StatsContainer : UIContainer
{
	// Called when the node enters the scene tree for the first time.
	[Export] public StatResource PlayerStats;
	[Export] public TextureProgressBar HealthBar;
	[Export] public Label StrengthLabel;
	[Export] public Label EnemyLabel;

	public override void _Ready()
	{
		PlayerStats.HealthChanged += OnHealthChanged;
		PlayerStats.AttackChanged += OnAttackChanged;
		GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
		OnHealthChanged(PlayerStats.CurrentHealth, PlayerStats.MaxHealth, 0);
		OnAttackChanged(PlayerStats.AttackStrength);

	}

	private async void OnAttackChanged(int attack)
	{
		StrengthLabel.Text = attack.ToString();
		// Flash yellow, then back to white
		StrengthLabel.Modulate = new Color(1, 1, 0); // yellow
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		StrengthLabel.Modulate = new Color(1, 1, 1); // white
	}

	private async void HandleNewEnemyCount(int count)
	{
		EnemyLabel.Text = count.ToString();
		// Flash red, then back to white
		EnemyLabel.Modulate = new Color(1, 0.4f, 0.4f); // light red
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		EnemyLabel.Modulate = new Color(1, 1, 1); // white
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
		PlayerStats.HealthChanged -= OnHealthChanged;
		PlayerStats.AttackChanged -= OnAttackChanged;
		GameEvents.OnNewEnemyCount -= HandleNewEnemyCount;
	}
}
