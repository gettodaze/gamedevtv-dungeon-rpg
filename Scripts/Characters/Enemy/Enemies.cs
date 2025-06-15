using Godot;
using System;

public partial class Enemies : Node
{
	private int _enemyCount = 0;
	private int EnemyCount
	{
		get => _enemyCount; set
		{
			if (_enemyCount == value) return;
			_enemyCount = value;
			GameEvents.RaiseNewEnemyCount(_enemyCount);
		}
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ChildExitingTree += HandleChildExitingTree;
		ChildEnteredTree += HandleChildEnteredTree;
		EnemyCount = GetChildCount();

	}

	private void HandleChildEnteredTree(Node node)
	{
		EnemyCount += 1;
		Log($"HandleChildEnteredTree new enemyCount {EnemyCount}");
	}

	private void HandleChildExitingTree(Node node)
	{
		EnemyCount -= 1;
		Log($"HandleChildExitingTree new enemyCount {EnemyCount}");
		if (EnemyCount == 0) GameEvents.RaiseVictory();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Log(object msg)
	{
		GD.Print($"{Name}: {msg}");
	}


}
