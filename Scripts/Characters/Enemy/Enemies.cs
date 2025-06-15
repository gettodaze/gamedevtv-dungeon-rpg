using Godot;
using System;

public partial class Enemies : Node
{
	private int enemyCount = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ChildExitingTree += HandleChildExitingTree;
		ChildEnteredTree += HandleChildEnteredTree;
		enemyCount = GetChildCount();
	}

	private void HandleChildEnteredTree(Node node)
	{
		enemyCount += 1;
		Log($"HandleChildEnteredTree new enemyCount {enemyCount}");
	}

	private void HandleChildExitingTree(Node node)
	{
		enemyCount -= 1;
		Log($"HandleChildExitingTree new enemyCount {enemyCount}");
		if (enemyCount == 0) GameEvents.RaiseVictory();
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
